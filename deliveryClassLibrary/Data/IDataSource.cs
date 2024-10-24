using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Data
{
    /// <summary>
    /// Интерфейс для доступа к данным
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataSource<T>
    {
        IEnumerable<T> GetAll();
        void Save(IEnumerable<T> items);
    }
}
