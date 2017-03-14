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

        private VenueWorkflowJobConfig PushOrderJobUSA
            => new VenueWorkflowJobConfig(
                venue: new Venue(Amazon.USASellingVenueId),
                jobType: typeof(WorkflowJob<
                                    PushOrderWorkflow<
                                        PushOrderServiceUSA>>));

        private VenueWorkflowJobConfig PushOrderJobCanada
            => new VenueWorkflowJobConfig(
                venue: new Venue(Amazon.CanadaSellingVenueId),
                jobType: typeof(WorkflowJob<
                                    PushOrderWorkflow<
                                        PushOrderServiceCanada>>));

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AmazonClient>();

            Register(builder, PushOrderJobUSA);
            Register(builder, PushOrderJobCanada);

            base.Load(builder);
        }
    }
}
