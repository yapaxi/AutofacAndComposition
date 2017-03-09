using Autofac;
using Autofac.Core;
using AutofacAndComposition.App.Client;
using AutofacAndComposition.App.Services;
using AutofacAndComposition.Model;
using AutofacAndComposition.AutofacModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using AutofacAndComposition.Quartz;
using Quartz.Impl;
using System.Diagnostics;
using Quartz.Spi;
using AutofacAndComposition.App.Configuration;
using AutofacAndComposition.App.DomainModel;

namespace AutofacAndComposition
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DataAccessModule());
            builder.RegisterModule(new AmazonModule());

            var container = builder.Build();

            using (var rootScope = container.BeginLifetimeScope("New Root"))
            {
                var scheduler = new StdSchedulerFactory().GetScheduler();
                scheduler.JobFactory = new QuartzJobFactory(rootScope);

                ScheduleVendor<Amazon>(scheduler, rootScope);

                scheduler.Start();
                Console.WriteLine("All done. Running.");
                Process.GetCurrentProcess().WaitForExit();
            }
        }

        private static void ScheduleVendor<TVendor>(IScheduler scheduler, ILifetimeScope scope)
            where TVendor : Vendor
        {
            var vendorName = typeof(TVendor).Name;
            var vendorConfigurationService = scope.Resolve<VendorConfigurationService>();

            Console.WriteLine($"[{vendorName}] Configuration started");
            
            foreach (var jobConfig in scope.ResolveNamed<IEnumerable<WorkflowJobConfig>>(vendorName))
            {
                Console.WriteLine($"[{vendorName}] Configuring job {jobConfig.JobType.Name}");

                var configurations = vendorConfigurationService.GetConfigurations<TVendor>();

                foreach (var configuration in configurations)
                {
                    Console.WriteLine($"[{vendorName}] Configuring job {jobConfig.JobType.Name} venue instance: id={configuration.Venue.Id};credential={configuration.Credential.Token}");

                    ScheduleJob(scheduler, jobConfig, configuration);
                }
            }
        }

        private static void ScheduleJob(IScheduler scheduler, WorkflowJobConfig jobConfig, VendorConfiguration configuration)
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() { { "Config", configuration } };

            var job = JobBuilder
                .Create(jobConfig.JobType)
                .SetJobData(new JobDataMap(dict))
                .Build();

            var trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }

    public class QuartzJobFactory : IJobFactory
    {
        private readonly ILifetimeScope _container;
        private readonly Dictionary<IJob, ILifetimeScope> _scopes;
        private readonly object _lock;

        public QuartzJobFactory(ILifetimeScope container)
        {
            _lock = new object();
            _scopes = new Dictionary<IJob, ILifetimeScope>();
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            IJob job = null;
            ILifetimeScope scope = null;
            try
            {
                if (bundle == null)
                {
                    throw new ArgumentNullException(nameof(bundle));
                }

                var config = (VendorConfiguration)bundle.JobDetail.JobDataMap["Config"];
                scope = _container.BeginLifetimeScope(builder => 
                {
                    builder.RegisterInstance(config.Venue).As<Venue>().SingleInstance();
                    builder.RegisterInstance(config.Credential).As<Credential>().SingleInstance();
                });

                job = (IJob)scope.Resolve(bundle.JobDetail.JobType);

                lock (_lock)
                {
                    _scopes.Add(job, scope);
                }

                return job;
            }
            catch (Exception ex)
            {
                scope?.Dispose();

                if (job != null)
                {
                    lock (_lock)
                    {
                        _scopes.Remove(job);
                    }
                }
                
                throw;
            }
        }

        public void ReturnJob(IJob job)
        {
            lock (_lock)
            {
                _scopes[job].Dispose();
                _scopes.Remove(job);
            }
        }
    }
}
