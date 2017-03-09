using AutofacAndComposition.App.DomainModel;
using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Repositories
{
    public class ConfigurationRepository
    {
        public IQueryable<Credential> Credentials { get; } = (new Credential[]
        {
            new Credential() { VendorCode = nameof(Amazon), SellingVenueId = Amazon.USASellingVenueId, Token = "usa1" },
            new Credential() { VendorCode = nameof(Amazon), SellingVenueId = Amazon.USASellingVenueId, Token = "usa2" },
            new Credential() { VendorCode = nameof(Amazon), SellingVenueId = Amazon.USASellingVenueId, Token = "usa3" },
            new Credential() { VendorCode = nameof(Amazon), SellingVenueId = Amazon.CanadaSellingVenueId, Token = "ca1" },
            new Credential() { VendorCode = nameof(Amazon), SellingVenueId = Amazon.CanadaSellingVenueId, Token = "ca2" },
        }).AsQueryable();
    }
}