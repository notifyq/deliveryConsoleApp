using deliveryClassLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.OrdersGeneration
{
    /// <summary>
    /// Генератор заказов. Необходимо для тестирования
    /// </summary>
    internal static class OrderGenerator
    {
        private readonly static List<string> _districts = new List<string>
        {
            // Районы города Уфа
            "Центральный",
            "Калининский",
            "Октябрьский",
            "Советский",
            "Демский",
            "Инорс"
        };

        public static List<Order> GenerateOrders(int count)
        {
            var orders = new List<Order>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                var order = new Order
                {
                    OrderId = i + 1,
                    Weight = Math.Round(random.NextDouble() * 10, 2),
                    OrderPoint = GetRandomDistrict(random),
                    DeliveryTime = DateTime.Now.AddMinutes(random.Next(-30, 120)) // добавляем случайное количество минут к текущему времени
                };
                orders.Add(order);
            }

            return orders;
        }
        private static string GetRandomDistrict(Random random)
        {
            return _districts[random.Next(_districts.Count)];
        }
    }
}
