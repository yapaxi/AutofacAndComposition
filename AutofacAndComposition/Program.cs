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

            foreach (var amazon in new Amazon[] { new AmazonX(), new AmazonY() })
            {
                foreach (var token in new[] { new Token("a"), new Token("b") })
                {
                    using (var scope = container.BeginLifetimeScope((scopeBuilder) =>
                    {
                        scopeBuilder.RegisterInstance(token);
                        scopeBuilder.RegisterInstance(amazon);
                    }))
                    {
                        var createOrderService = scope.Resolve<ICreateOrderService<Amazon>>();
                        createOrderService.CreateOrders();
                    }
                }
            }
        }


    }

    //public class RootModule<TVenue>: Module
    //    where TVenue : Amazon
    //{
    //    protected override void Load(ContainerBuilder builder)
    //    {
    //        builder.RegisterType<OrderService<TVenue, AmazonClient>>().As<ICreateOrderService<TVenue>>();
    //        builder.RegisterType<AmazonClient>();
    //        base.Load(builder);
    //    }
    //}

    //public class OrderService<TVenue, TClient> : ICreateOrderService<TVenue>
    //    where TVenue : Venue
    //    where TClient : IClient
    //{
    //    private TClient _client;

    //    public OrderService(TClient client)
    //    {
    //        _client = client;
    //    }

    //    public void CreateOrders()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}
