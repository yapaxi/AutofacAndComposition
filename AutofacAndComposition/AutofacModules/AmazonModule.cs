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
    public sealed class AmazonModule : SaleVendorModuleTemplate<Amazon>
    {
        protected override WorkflowJobConfig CreateOrderJob 
            => new WorkflowJobConfig(typeof(WorkflowJob<
                                                CreateOrderWorkflow<
                                                    CreateOrderService>>));

        private VenueWorkflowJobConfig R1CreateOrderJobUSA
            => new VenueWorkflowJobConfig(new Venue(Amazon.USASellingVenueId), typeof(WorkflowJob<
                                                R1CreateOrderWorkflow<
                                                    R1CreateOrderServiceUSA>>));

        private VenueWorkflowJobConfig R1CreateOrderJobCanada
            => new VenueWorkflowJobConfig(new Venue(Amazon.CanadaSellingVenueId), typeof(WorkflowJob<
                                                R1CreateOrderWorkflow<
                                                    R1CreateOrderServiceCanada>>));

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AmazonClient>();

            Register(builder, R1CreateOrderJobUSA);
            Register(builder, R1CreateOrderJobCanada);

            base.Load(builder);
        }
    }
}
