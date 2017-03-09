using Autofac;
using Autofac.Core;
using AutofacAndComposition.App.Client;
using AutofacAndComposition.App.Services;
using AutofacAndComposition.Model;
using AutofacAndComposition.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterModule(new AmazonModuleTemplate());

            var container = builder.Build();

            foreach (var nbd in container.Resolve<IEnumerable<ScopeDependency>>())
            {
                using (var scope = container.BeginLifetimeScope((scopeBuilder) =>
                {
                    foreach (var d in nbd.Deps)
                    {
                        scopeBuilder.RegisterInstance(d.instance).As(d.type);
                    }
                }))
                {
                    var createOrderService = scope.Resolve<ICreateOrderService<Amazon>>();
                    createOrderService.CreateOrders();
                }
            }
        }
    }
}
