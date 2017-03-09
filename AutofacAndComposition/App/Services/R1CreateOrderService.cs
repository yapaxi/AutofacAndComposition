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
    public class R1CreateOrderServiceUSA : IR1CreateOrderService
    {
        private readonly R1Client _client;
        private readonly Venue _venue;

        public R1CreateOrderServiceUSA(Venue venue, R1Client client)
        {
            _client = client;
            _venue = venue;
        }

        public void CreateOrder(Order order)
        {
            Console.WriteLine($"[USA] Creating order in r1");
            _client.CreateOrder(order);
        }
    }

    public class R1CreateOrderServiceCanada : IR1CreateOrderService
    {
        private readonly R1Client _client;
        private readonly Venue _venue;

        public R1CreateOrderServiceCanada(Venue venue, R1Client client)
        {
            _client = client;
            _venue = venue;
        }

        public void CreateOrder(Order order)
        {
            Console.WriteLine($"[CANADA] Creating order in r1");
            _client.CreateOrder(order);
        }
    }
}
