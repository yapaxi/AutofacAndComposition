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
    public class PushOrderWorkflow<TPushOrderService> : IWorkflow
        where TPushOrderService : IPushOrderService
    {
        private readonly TPushOrderService _orderCreator;
        private readonly Venue _venue;
        private readonly OrderRepository _repository;

        public PushOrderWorkflow(Venue venue, TPushOrderService orderCreator, OrderRepository repository)
        {
            _orderCreator = orderCreator;
            _venue = venue;
            _repository = repository;
        }

        public void Run()
        {
            var orders = _repository.Orders.Where(e => e.SellingVenueId == _venue.Id);

            foreach (var order in orders)
            {
                try
                {
                    Console.WriteLine($"[{_venue.Id}] Pusing order \"{order}\" somewhere...");
                    _orderCreator.PushOrder(order);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
