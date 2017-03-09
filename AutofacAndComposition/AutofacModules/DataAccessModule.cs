using Autofac;
using AutofacAndComposition.App.Repositories;
using AutofacAndComposition.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.AutofacModules
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<VendorConfigurationService>().InstancePerLifetimeScope();


            builder.RegisterType<OrderRepository>().SingleInstance(); //only to preserve changed in memory between runs! Real registration must be  InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
