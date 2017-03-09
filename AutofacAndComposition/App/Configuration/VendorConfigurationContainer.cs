using AutofacAndComposition.App.DomainModel;
using AutofacAndComposition.App.Repositories;
using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Configuration
{
    public class VendorConfigurationContainer
    {
        private readonly ConfigurationRepository _repository;

        public VendorConfigurationContainer(ConfigurationRepository repository)
        {
            _repository = repository;
        }

        public VendorConfiguration[] Resolve<TVendor>()
            where TVendor : Vendor
        {
            var vendorCode = typeof(TVendor).Name; 
            var credentials = _repository.Credentials.Where(e => e.VendorCode == vendorCode).ToArray();
            
            return credentials.Select(e => new VendorConfiguration(new Venue(e.SellingVenueId), e)).ToArray();
        }
    }
}
