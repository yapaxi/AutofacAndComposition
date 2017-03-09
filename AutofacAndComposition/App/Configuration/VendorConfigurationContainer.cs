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
    public class VendorConfigurationService
    {
        private readonly ConfigurationRepository _repository;

        public VendorConfigurationService(ConfigurationRepository repository)
        {
            _repository = repository;
        }

        public VendorConfiguration[] GetConfigurations<TVendor>()
            where TVendor : Vendor
        {
            var vendorCode = typeof(TVendor).Name; 
            var credentials = _repository.Credentials.Where(e => e.VendorCode == vendorCode).ToArray();
            
            return credentials.Select(e => new VendorConfiguration(new Venue(e.SellingVenueId), e)).ToArray();
        }
    }
}
