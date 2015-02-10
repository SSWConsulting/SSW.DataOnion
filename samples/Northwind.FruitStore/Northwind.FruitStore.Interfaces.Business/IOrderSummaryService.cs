using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.FruitStore.Interfaces.Business
{
    public interface IOrderSummaryService
    {
        void SendOrderSummary(string toEmailAddress, DateTime startDate, DateTime endDate);
    }
}
