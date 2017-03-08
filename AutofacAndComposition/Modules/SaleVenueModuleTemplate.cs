using Autofac;
using Autofac.Core;
using AutofacAndComposition.App.Services;
using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.Modules
{
    public abstract class SaleVenueModuleTemplate<TVenue> : Module
        where TVenue : Venue
    {
        public Type VenueType { get; } = typeof(TVenue);
        
        protected abstract Type CreateOrderService { get; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(CreateOrderService).As<ICreateOrderService<TVenue>>();
            builder.RegisterType<TVenue>();
            base.Load(builder);

        }
    }
}
