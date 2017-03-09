using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutofacAndComposition.App.Client;
using AutofacAndComposition.App.Services;

namespace AutofacAndComposition.Modules
{
    public class AmazonModuleTemplate : SaleVenueModuleTemplate<Amazon>
    {
        private readonly (Amazon venue, Token token)[] _nonBehavioralDependecies = new (Amazon, Token)[]
        {
                (new AmazonX(), new Token("a2")),
                (new AmazonY(), new Token("b"))
        };

        protected override Type CreateOrderService => typeof(CreateOrderService<Amazon, AmazonClient>);

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AmazonClient>();

            foreach (var nbd in _nonBehavioralDependecies)
            {
                builder.RegisterInstance(new ScopeDependency(new (object, Type)[] 
                {
                    (nbd.venue, typeof(Amazon)),
                    (nbd.token, typeof(Token)),
                }));
            }

            base.Load(builder);
        }
    }
}
