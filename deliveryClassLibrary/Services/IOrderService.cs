using deliveryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Services
{
    public interface IOrderService
    {
        void GenerateAndSaveOrders(int count);
        List<Order> GetFilteredOrders(string orderPoint, DateTime startTime);
    }
}
