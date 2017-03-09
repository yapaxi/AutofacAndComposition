using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.Model
{
    public class ScopeDependency
    {
        public (object instance, Type type)[] Deps { get; }

        public ScopeDependency((object instance, Type type)[] deps)
        {
            Deps = deps;
        }
    }
}
