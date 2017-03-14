using AutofacAndComposition.App.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Client
{
    public class SomeMicroserviceClient
    {
        private readonly object _lock = new object();
        private readonly HashSet<string> _orders = new HashSet<string>();

        public void PushOrder(Order order)
        {
            lock (_lock)
            {
                if (!_orders.Contains(order.VenueOrderId))
                {
                    _orders.Add(order.VenueOrderId);
                    Console.WriteLine($"[{nameof(SomeMicroserviceClient)}] Pusing order \"{order.VenueOrderId}\" somewhere...");
                }
                else
                {
                    Console.WriteLine($"[{nameof(SomeMicroserviceClient)}] Order \"{order.VenueOrderId}\" is already pushed somewhere...");
                }
            }
        }
    }
}
