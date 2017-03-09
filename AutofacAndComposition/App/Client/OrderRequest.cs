using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Client
{
    public class OrderRequest
    {
        public string VenueOrderId { get; set; }

        public override string ToString() => VenueOrderId;
    }
}
