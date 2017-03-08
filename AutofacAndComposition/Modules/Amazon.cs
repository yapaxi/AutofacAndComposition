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
        protected override Type CreateOrderService => typeof(CreateOrderService<Amazon, AmazonClient>);

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AmazonClient>();
            base.Load(builder);
        }
    }
}
