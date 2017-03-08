using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Client
{
    public class AmazonClient : IClient
    {
        private readonly Token _token;

        public AmazonClient(Token token)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public string TokenValue => _token.TokenValue;

        public object[] GetOrders()
        {
            return new object[] { 1, 2, 3, 4 };
        }
    }
}
