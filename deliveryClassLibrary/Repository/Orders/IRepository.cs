using deliveryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Repository.Orders
{
    public interface IRepository<T>
    {
        List<Order> GetList();
        void Save(List<Order> orders);
    }
}
