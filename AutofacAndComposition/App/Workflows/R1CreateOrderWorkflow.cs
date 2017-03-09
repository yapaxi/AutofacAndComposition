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
    public class R1CreateOrderWorkflow<TR1CreateOrderService> : IWorkflow
        where TR1CreateOrderService : IR1CreateOrderService
    {
        private readonly TR1CreateOrderService _orderCreator;
        private readonly Venue _venue;
        private readonly OrderRepository _repository;

        public R1CreateOrderWorkflow(Venue venue, TR1CreateOrderService orderCreator, OrderRepository repository)
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
                    Console.WriteLine($"[{_venue.Id}] Sending order \"{order}\" to R1");
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
