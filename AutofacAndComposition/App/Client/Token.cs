using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Client
{
    public class Token
    {
        public Token(string value)
        {
            TokenValue = value;
        }

        public string TokenValue { get; }
    }
}
