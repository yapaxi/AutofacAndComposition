using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Client
{
    public interface IClient
    {
        string TokenValue { get; }
        object[] GetOrders();
    }
}
