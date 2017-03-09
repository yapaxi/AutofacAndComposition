using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.DomainModel
{
    public class Credential
    {
        public int Id { get; set; }

        public string VendorCode { get; set; }

        public int SellingVenueId { get; set; }

        public string Token { get; set; }
    }
}
