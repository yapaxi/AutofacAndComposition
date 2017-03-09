using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutofacAndComposition.App.Client;
using AutofacAndComposition.App.Services;
using AutofacAndComposition.App.Workflows;
using AutofacAndComposition.Quartz;

namespace AutofacAndComposition.AutofacModules
{
    public class AmazonModule : SaleVendorModuleTemplate<Amazon>
    {
        protected override Type CreateOrderJob => typeof(WorkflowJob<
                                                            CreateOrderWorkflow<
                                                                CreateOrderService>>);

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AmazonClient>();
            base.Load(builder);
        }
    }
}
