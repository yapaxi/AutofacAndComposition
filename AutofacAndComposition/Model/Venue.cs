using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.Model
{
    // abstract

    public class Venue
    {
        public Venue (int id)
        {
            Id = id;
        }

        public int Id { get; }

        public override string ToString() => $"Venue-{Id}";
    }
    
    public abstract class Vendor
    {

    }

    public class Amazon : Vendor
    {
        public const int USASellingVenueId = 101;
        public const int CanadaSellingVenueId = 102;
    }
}
