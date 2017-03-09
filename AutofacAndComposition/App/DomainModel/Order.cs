using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.DomainModel
{
    public class Order
    {
        public int Id { get; set; }

        public int SellingVenueId { get; set; }

        public string VenueOrderId { get; set; }

        public override string ToString() => $"Id={Id};SellingVenueId={SellingVenueId};VenueOrderId={VenueOrderId}";
    }
}
