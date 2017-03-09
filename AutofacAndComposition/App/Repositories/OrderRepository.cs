using AutofacAndComposition.App.DomainModel;
using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Repositories
{
    public class OrderRepository
    {
        private List<Order> _lst = new List<Order>();

        public IQueryable<Order> Orders => (new[] 
        {
            new Order() { Id = 1, SellingVenueId = Amazon.USASellingVenueId, VenueOrderId = "1" },
            new Order() { Id = 2, SellingVenueId = Amazon.USASellingVenueId, VenueOrderId = "2" },
            new Order() { Id = 3, SellingVenueId = Amazon.USASellingVenueId , VenueOrderId = "3"},

            new Order() { Id = 4, SellingVenueId = Amazon.CanadaSellingVenueId, VenueOrderId = "1" },
            new Order() { Id = 5, SellingVenueId = Amazon.CanadaSellingVenueId, VenueOrderId = "2" },
        }.Concat(_lst)).AsQueryable();

        internal void AddOrder(Order order)
        {
            order.Id = Orders.Max(e => e.Id) + 1;

            _lst.Add(order);
        }
    }
}
