using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.Model
{
    // abstract

    public abstract class Venue { public abstract int Id { get; } }

    public abstract class Amazon : Venue { }
    
    // concrect
    
    public class AmazonX : Amazon { public override int Id { get; } = 101; }

    public class AmazonY : Amazon { public override int Id { get; } = 102; }
}
