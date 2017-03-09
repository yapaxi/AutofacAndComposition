﻿using AutofacAndComposition.App.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.Model
{
    public class VendorConfiguration
    {
        public Venue Venue { get; }

        public Credential Credential { get; }
        
        public VendorConfiguration(Venue venue, Credential credential)
        {
            Venue = venue;
            Credential = credential;
        }
    }
}
