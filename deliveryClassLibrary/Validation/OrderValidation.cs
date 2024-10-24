using deliveryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Validation
{
    /// <summary>
    /// Валидация заказа
    /// </summary>
    /// 
    public class OrderValidation: IModelValidation<Order>
    {
        public bool IsValid(Order order)
        {
            if (order == null)
            {
                return false;
            }
            if (order.OrderId <= 0)
            {
                return false;
            }
            if (order.Weight <= 0)
            {
                return false;
            }
            if (string.IsNullOrEmpty(order.OrderPoint))
            {
                return false;
            }
            return true;
        }
    }
}
