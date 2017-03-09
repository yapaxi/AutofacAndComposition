using AutofacAndComposition.App.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Client
{
    public class R1Client
    {
        private readonly object _lock = new object();
        private readonly HashSet<string> _orders = new HashSet<string>();

        public void CreateOrder(Order order)
        {
            lock (_lock)
            {
                if (!_orders.Contains(order.VenueOrderId))
                {
                    _orders.Add(order.VenueOrderId);
                    Console.WriteLine($"Created order \"{order.VenueOrderId}\" in R1");
                }
                else
                {
                    Console.WriteLine($"Order \"{order.VenueOrderId}\" is already created in R1");
                }
            }
        }
    }
}
