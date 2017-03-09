using AutofacAndComposition.App.Client;
using AutofacAndComposition.App.Repositories;
using AutofacAndComposition.App.Services;
using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Workflows
{
    public class CreateOrderWorkflow<TCreateOrderService> : IWorkflow
        where TCreateOrderService : ICreateOrderService
    {
        private readonly TCreateOrderService _orderCreator;
        private readonly AmazonClient _client;
        private readonly Venue _venue;

        public CreateOrderWorkflow(Venue venue, TCreateOrderService orderCreator, AmazonClient client)
        {
            _orderCreator = orderCreator;
            _client = client;
            _venue = venue;
        }

        public void Run()
        {
            var orders = _client.GetOrders();

            foreach (var order in orders)
            {
                try
                {
                    Console.WriteLine($"[{_venue.Id}][{_client}] Processing order request \"{order}\"");
                    _orderCreator.CreateOrder(order);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
