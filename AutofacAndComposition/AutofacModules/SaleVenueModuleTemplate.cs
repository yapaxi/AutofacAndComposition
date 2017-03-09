using Autofac;
using Autofac.Core;
using AutofacAndComposition.App.Services;
using AutofacAndComposition.Model;
using AutofacAndComposition.Quartz;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.AutofacModules
{
    public abstract class SaleVendorModuleTemplate<TVendor> : Module
        where TVendor : Vendor
    {
        protected abstract WorkflowJobConfig CreateOrderJob { get; }
        
        protected override void Load(ContainerBuilder builder)
        {
            Register(builder, CreateOrderJob);
            base.Load(builder);
        }

        protected void Register(ContainerBuilder builder, WorkflowJobConfig config)
        {
            builder.RegisterInstance(config).Named<WorkflowJobConfig>(typeof(TVendor).Name);
            RecursiveRegister(builder, config.JobType);
        }

        private void RecursiveRegister(ContainerBuilder builder, Type type)
        {
            if (type.IsGenericType)
            {
                foreach (var t in type.GetGenericArguments())
                {
                    RecursiveRegister(builder, t);
                }
            }

            builder.RegisterType(type).PreserveExistingDefaults();
        }
    }


}
