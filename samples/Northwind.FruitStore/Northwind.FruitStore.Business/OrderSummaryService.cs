using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.FruitStore.Interfaces.Business;
using Northwind.FruitStore.Interfaces.Repositories;
using SSW.Common;

namespace Northwind.FruitStore.Business
{
    public class OrderSummaryService : IOrderSummaryService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationProvider _notificationProvider;
        private readonly ILogger _logger;

        public OrderSummaryService(IOrderRepository orderRepository, INotificationProvider notificationProvider, ILogger logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _notificationProvider = notificationProvider;
        }

        public void SendOrderSummary(string toEmailAddress, DateTime startDate, DateTime endDate)
        {
            //Discussion point: the point at which this moves to the repository
            var orders =
                _orderRepository.Get(o => o.OrderDate >= startDate && o.OrderDate < endDate)
                    .OrderBy(c => c.OrderDate)
                    .Select(o => new {Code = o.OrderCode, Status = o.OrderStatus, Date = o.OrderDate}).ToList();

            var messageSubject = string.Format("Order Summary {0} - {1}", startDate, endDate);

            var messageBody = new System.Text.StringBuilder();

            orders.ForEach(c => messageBody.AppendLine(string.Format("{0} - {1} ({2})", c.Date, c.Code, c.Status)));

            _notificationProvider.Send(toEmailAddress, messageSubject, messageBody.ToString());

        }
    }
}
