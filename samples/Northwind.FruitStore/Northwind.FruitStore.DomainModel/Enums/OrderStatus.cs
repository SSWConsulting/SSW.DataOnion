using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet;

namespace Northwind.FruitStore.DomainModel
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Cancelled
    }
}
