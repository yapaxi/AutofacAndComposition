using Autofac;
using AutofacAndComposition.App.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.AutofacModules
{
    public class SomeMicroserviceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SomeMicroserviceClient>().SingleInstance();
            base.Load(builder);
        }
    }
}
