using Autofac;
using AutofacAndComposition.App.DomainModel;
using AutofacAndComposition.Model;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.Quartz
{
    public class LifetimeScopeJobFactory : IJobFactory
    {
        private readonly ILifetimeScope _container;
        private readonly Dictionary<IJob, ILifetimeScope> _scopes;
        private readonly object _lock;

        public LifetimeScopeJobFactory(ILifetimeScope container)
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

                var lateDependency = bundle.JobDetail.JobDataMap.TryGetLateDependencyBundle();
                if (lateDependency != null)
                {
                    scope = _container.BeginLifetimeScope(builder => lateDependency.Register(builder));
                }
                else
                {
                    scope = _container.BeginLifetimeScope();
                }
                
                job = (IJob)scope.Resolve(bundle.JobDetail.JobType);

                lock (_lock)
                {
                    _scopes.Add(job, scope);
                }

                return job;
            }
            catch
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
