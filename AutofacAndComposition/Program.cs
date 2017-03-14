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
using AutofacAndComposition.App.DomainModel;
using static AutofacAndComposition.Tools;

namespace AutofacAndComposition
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootScopeTag = "New Root";
            var builder = new ContainerBuilder();

            builder.RegisterModule(new SomeMicroserviceModule());
            builder.RegisterModule(new DataAccessModule());
            builder.RegisterModule(new AmazonModule());
            builder.RegisterType<LifetimeScopeJobFactory>().As<IJobFactory>().InstancePerMatchingLifetimeScope(rootScopeTag);
            
            var container = builder.Build();

            using (var rootScope = container.BeginLifetimeScope(rootScopeTag))
            {
                var scheduler = new StdSchedulerFactory().GetScheduler();
                scheduler.JobFactory = rootScope.Resolve<IJobFactory>();
                scheduler.Start();

                ScheduleVendor<Amazon>(scheduler, rootScope);

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
                Console.WriteLine($"  [{vendorName}] Configuring {GetWorkflowName(jobConfig.JobType)} ");

                VendorConfiguration[] configurations;

                switch (jobConfig)
                {
                    case VenueWorkflowJobConfig vconf:
                        Console.WriteLine($"    [{vendorName}] Workflow {GetWorkflowName(jobConfig.JobType)} is restricted to venue {vconf.Venue}");
                        configurations = vendorConfigurationService.GetConfigurations<TVendor>(vconf.Venue);
                        break;
                    case WorkflowJobConfig conf:
                        Console.WriteLine($"    [{vendorName}] Workflow {GetWorkflowName(jobConfig.JobType)} is not restricted to venue");
                        configurations = vendorConfigurationService.GetConfigurations<TVendor>();
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                foreach (var configuration in configurations)
                {
                    Console.WriteLine($"        [{vendorName}] Configuring workflow {GetWorkflowName(jobConfig.JobType)} venue instance: id={configuration.Venue.Id};credential={configuration.Credential.Token}");

                    ScheduleJob(scheduler, jobConfig, configuration);
                }
            }
        }

        private static void ScheduleJob(IScheduler scheduler, WorkflowJobConfig jobConfig, VendorConfiguration configuration)
        {
            var map = new JobDataMap();
            map.SetLateDependencyBundle(configuration);

            var job = JobBuilder
                .Create(jobConfig.JobType)
                .SetJobData(map)
                .Build();

            var trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInSeconds(60)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);

            //job resolving is in Quartz\LifetimeScopeJobFactory.cs
        }
    }
}
