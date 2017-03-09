using AutofacAndComposition.App.DomainModel;
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

        public (string name, object instance, Type type)[] Flatten()
        {
            IEnumerable<(string name, object instance, Type type)> func()
            {
                yield return (nameof(Credential), Credential, typeof(Credential));
                yield return (nameof(Venue), Venue, typeof(Venue));
            }

            return func().ToArray();
        }
    }
}
