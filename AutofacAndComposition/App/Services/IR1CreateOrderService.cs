﻿using AutofacAndComposition.App.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.App.Services
{
    public interface IR1CreateOrderService
    {
        void CreateOrder(Order order);
    }
}
