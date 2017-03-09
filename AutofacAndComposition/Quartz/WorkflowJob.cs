using AutofacAndComposition.App.Workflows;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.Quartz
{
    [DisallowConcurrentExecution]
    public class WorkflowJob<TWorkflow> : IJob
        where TWorkflow : IWorkflow
    {
        private readonly TWorkflow _workflow;

        public WorkflowJob(TWorkflow workflow)
        {
            _workflow = workflow;
        }

        public void Execute(IJobExecutionContext context)
        {
            _workflow.Run();
        }
    }
}
