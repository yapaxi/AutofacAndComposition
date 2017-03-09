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
        protected abstract Type CreateOrderJob { get; }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new WorkflowJobConfig(CreateOrderJob)).Named<WorkflowJobConfig>(typeof(TVendor).Name);
            RecursiveRegister(builder, CreateOrderJob);
            base.Load(builder);
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
