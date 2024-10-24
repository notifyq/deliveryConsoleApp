using deliveryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Filtration
{
    public static class OrderFilter
    {
        // Пока не особо понимаю как лучше реализовать фильтрацию.
        public static List<Order> FilterOrders(List<Order> orders, string orderPoint, DateTime startTime)
        {
            
            if (orders == null) throw new ArgumentNullException(nameof(orders));

            var endTime = startTime.AddMinutes(30);

            return orders.Where(o =>
                o.OrderPoint.Equals(orderPoint, StringComparison.OrdinalIgnoreCase) &&
                o.DeliveryTime >= startTime &&
                o.DeliveryTime <= endTime
            ).ToList();
        }
    }
}
