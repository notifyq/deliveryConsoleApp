using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deliveryClassLibrary;
using deliveryClassLibrary.Filtration;
using deliveryClassLibrary.Models;
using Xunit;

namespace deliveryTests
{
    public class FilterTests
    {
        [Fact]
        public void FilterOrders_ShouldReturnMatchingOrders()
        {
            var orders = new List<Order>
            {
                new Order { OrderId = 1, Weight = 10, OrderPoint = "Центральный", DeliveryTime = DateTime.Parse("2024-10-23 12:00:00") },
                new Order { OrderId = 2, Weight = 5, OrderPoint = "Демский", DeliveryTime = DateTime.Parse("2024-10-23 12:15:00") },
                new Order { OrderId = 3, Weight = 7, OrderPoint = "Центральный", DeliveryTime = DateTime.Parse("2024-10-23 12:20:00") },
            };

            var filteredOrders = OrderFilter.FilterOrders(orders, "Центральный", DateTime.Parse("2024-10-23 12:00:00"));

            Assert.Equal(2, filteredOrders.Count);
        }

        [Fact]
        public void FilterOrders_ShouldReturnNoOrders_WhenNoneMatch()
        {
            var orders = new List<Order>
            {
                new Order { OrderId = 1, Weight = 10, OrderPoint = "Центральный", DeliveryTime = DateTime.Parse("2024-10-23 12:00:00") },
            };

            var filteredOrders = OrderFilter.FilterOrders(orders, "Демский", DateTime.Parse("2024-10-23 12:00:00"));

            Assert.Empty(filteredOrders);
        }
    }
}
