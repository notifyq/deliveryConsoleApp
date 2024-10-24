using deliveryClassLibrary.Filtration;
using deliveryClassLibrary.Logger;
using deliveryClassLibrary.Models;
using deliveryClassLibrary.OrdersGeneration;
using deliveryClassLibrary.Repository.Orders;
using deliveryClassLibrary.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Services
{
    public class OrderService: IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IModelValidation<Order> _validator;
        private readonly ILogger _logger;
        public OrderService(IRepository<Order> orderRepository, IModelValidation<Order> validator, ILogger logger)
        {
            _orderRepository = orderRepository;
            _validator = validator;
            _logger = logger;
            
        }

        public void GenerateAndSaveOrders(int count)
        {
            var orders = OrderGenerator.GenerateOrders(count);

            foreach (var order in orders)
            {
                if (!_validator.IsValid(order))
                {
                    _logger.Log($"Недействительный заказ: {JsonConvert.SerializeObject(order)}", LogLevel.Error);
                }
            }
            _orderRepository.Save(orders);
            _logger.Log($"Сгенерировано {orders.Count} заказов");
        }

        public List<Order> GetFilteredOrders(string orderPoint, DateTime startTime)
        {
            _logger.Log("Фильтрация заказов");
            var orders = _orderRepository.GetList();
            if (orders == null)
            {
                _logger.Log("Не удалось получить список заказов при фильтрации", LogLevel.Error);
                return null;
            }
            orders = OrderFilter.FilterOrders(orders, orderPoint, startTime);
            return orders;
        }

    }
   
}
