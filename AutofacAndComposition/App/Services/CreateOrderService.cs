using AutofacAndComposition.App.Client;
using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Services
{
    public class CreateOrderService<TVenue, TClient> : ICreateOrderService<TVenue>
        where TClient : IClient
        where TVenue : Venue
    {
        private readonly TVenue _venue;
        private readonly TClient _client;

        public CreateOrderService(TVenue venue, TClient client)
        {
            _venue = venue;
            _client = client;
        }

        public void CreateOrders() => SaveOrders(ProcessOrders(_client.GetOrders()));

        private object[] ProcessOrders(object[] orders) => orders;

        private void SaveOrders(object[] processedOrders) => processedOrders.ToList().ForEach(e => Console.WriteLine($"{_venue.Id} via token {_client.TokenValue} processes order {e}"));
    }
}
