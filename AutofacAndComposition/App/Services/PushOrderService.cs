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
    public class PushOrderServiceUSA : IPushOrderService
    {
        private readonly SomeMicroserviceClient _client;
        private readonly Venue _venue;

        public PushOrderServiceUSA(Venue venue, SomeMicroserviceClient client)
        {
            _client = client;
            _venue = venue;
        }

        public void PushOrder(Order order)
        {
            Console.WriteLine($"[USA] Pushing order somewhere...");
            _client.PushOrder(order);
        }
    }

    public class PushOrderServiceCanada : IPushOrderService
    {
        private readonly SomeMicroserviceClient _client;
        private readonly Venue _venue;

        public PushOrderServiceCanada(Venue venue, SomeMicroserviceClient client)
        {
            _client = client;
            _venue = venue;
        }

        public void PushOrder(Order order)
        {
            Console.WriteLine($"[CANADA] Pushing order somewhere...");
            _client.PushOrder(order);
        }
    }
}
