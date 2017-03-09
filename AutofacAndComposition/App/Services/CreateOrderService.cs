using AutofacAndComposition.App.Client;
using AutofacAndComposition.App.DomainModel;
using AutofacAndComposition.App.Repositories;
using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Services
{
    public class CreateOrderService : ICreateOrderService
    {
        private readonly OrderRepository _repository;
        private readonly Venue _venue;

        public CreateOrderService(Venue venue, OrderRepository repository)
        {
            _repository = repository;
            _venue = venue;
        }

        public void CreateOrder(OrderRequest request)
        {
            var exists = _repository.Orders.Where(e => e.SellingVenueId == _venue.Id && e.VenueOrderId == request.VenueOrderId).Any();

            if (exists)
            {
                Console.WriteLine($"[{_venue.Id}] Order request \"{request}\" is already processed");
                return;
            }

            var order = new Order() { SellingVenueId = _venue.Id, VenueOrderId = request.VenueOrderId };
            _repository.AddOrder(order);

            Console.WriteLine($"+++ [{_venue.Id}] Created order \"{order}\"");
        }
    }
}
