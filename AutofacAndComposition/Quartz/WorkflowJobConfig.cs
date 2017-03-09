using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.Quartz
{
    public class WorkflowJobConfig
    {
        public Type JobType { get; }

        public WorkflowJobConfig(Type jobType)
        {
            JobType = jobType;
        }
    }

    public class VenueWorkflowJobConfig : WorkflowJobConfig
    {
        public Venue Venue { get; }

        public VenueWorkflowJobConfig(Venue venue, Type jobType) 
            : base(jobType)
        {
            Venue = venue;
        }
    }
}
