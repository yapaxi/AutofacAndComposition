using AutofacAndComposition.App.Client;
using AutofacAndComposition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Services
{
    public interface ICreateOrderService
    {
        void CreateOrder(OrderRequest request);
    }
}
