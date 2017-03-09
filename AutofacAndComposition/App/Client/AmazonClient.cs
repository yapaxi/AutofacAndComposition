using AutofacAndComposition.App.DomainModel;
using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Client
{
    public class AmazonClient : IClient
    {
        private readonly Credential _credential;

        public AmazonClient(Credential credential)
        {
            _credential = credential ?? throw new ArgumentNullException(nameof(credential));
        }
        
        public OrderRequest[] GetOrders()
        {
            switch (_credential.SellingVenueId)
            {
                case Amazon.USASellingVenueId:
                    return new[]
                    {
                        new OrderRequest() { VenueOrderId = "1" },
                        new OrderRequest() { VenueOrderId = "2" },
                        new OrderRequest() { VenueOrderId = "3" },
                        new OrderRequest() { VenueOrderId = "4" },
                        new OrderRequest() { VenueOrderId = "5" },
                        new OrderRequest() { VenueOrderId = "6" },
                        new OrderRequest() { VenueOrderId = "7" },
                    };
                case Amazon.CanadaSellingVenueId:
                    return new[]
                    {
                        new OrderRequest() { VenueOrderId = "1" },
                        new OrderRequest() { VenueOrderId = "2" },
                        new OrderRequest() { VenueOrderId = "3" },
                        new OrderRequest() { VenueOrderId = "4" },
                    };
            }

            throw new InvalidOperationException();
        }

        public override string ToString() => $"{nameof(AmazonClient)} over token {_credential.Token}";
    }
}
