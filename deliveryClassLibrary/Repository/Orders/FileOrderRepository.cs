using deliveryClassLibrary.Data;
using deliveryClassLibrary.Logger;
using deliveryClassLibrary.Models;
using deliveryClassLibrary.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Repository.Orders
{   /// <summary>
    /// Репозиторий для работы с заказами
    /// </summary>
    public class FileOrderRepository : IRepository<Order>
    {
        private readonly IDataSource<Order> _dataSource;
        private readonly IModelValidation<Order> _validator;
        private readonly ILogger _logger;

        public FileOrderRepository(IDataSource<Order> dataSource, IModelValidation<Order> validator, ILogger logger)
        {
            _dataSource = dataSource;
            _validator = validator;
            _logger = logger;
        }

        public List<Order> GetList()
        {

            var orders = _dataSource.GetAll().ToList();

            foreach (var order in orders)
            {
                if (!_validator.IsValid(order))
                {
                    _logger.Log($"Недействительный заказ: {JsonConvert.SerializeObject(order)}", LogLevel.Error);
                }
            }
            return orders;
        }

        public void Save(List<Order> orders)
        {
            _dataSource.Save(orders);
        }
    }
}
