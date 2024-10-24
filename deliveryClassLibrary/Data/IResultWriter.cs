using deliveryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Data
{
    public interface IResultWriter
    {
        void WriteResult(IEnumerable<Order> items);
    }
}
