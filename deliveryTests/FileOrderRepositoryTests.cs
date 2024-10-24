using deliveryClassLibrary.Data;
using deliveryClassLibrary.Logger;
using deliveryClassLibrary.Models;
using deliveryClassLibrary.Repository.Orders;
using deliveryClassLibrary.Validation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryTests
{
    public class FileOrderRepositoryTests
    {
        [Fact]
        public void GetList_ShouldReturnOnlyValidOrders()
        {
            var mockDataSource = new Mock<IDataSource<Order>>();
            var mockValidator = new Mock<IModelValidation<Order>>();
            var mockLogger = new Mock<ILogger>();

            var orders = new List<Order>
            {
                new Order { OrderId = 1, Weight = 10, OrderPoint = "Демский", DeliveryTime = DateTime.Now },
                new Order { OrderId = 2, Weight = 5, OrderPoint = "Центральный", DeliveryTime = DateTime.Now },
            };

            //Настройка
            mockDataSource.Setup(ds => ds.GetAll()).Returns(orders);
            mockValidator.Setup(v => v.IsValid(It.IsAny<Order>())).Returns(true);

            var repository = new FileOrderRepository(mockDataSource.Object, mockValidator.Object, mockLogger.Object);
            var result = repository.GetList();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetList_ShouldLogInvalidOrders()
        {
            var mockDataSource = new Mock<IDataSource<Order>>();
            var mockValidator = new Mock<IModelValidation<Order>>();
            var mockLogger = new Mock<ILogger>();

            var orders = new List<Order>
            {
                new Order { OrderId = 0, Weight = 5, OrderPoint = "Демский", DeliveryTime = DateTime.Now }, // Невалидный заказ
            };

            mockDataSource.Setup(ds => ds.GetAll()).Returns(orders);
            mockValidator.Setup(v => v.IsValid(It.IsAny<Order>())).Returns((Order order) => order.OrderId > 0); // Только валидный заказ
            mockLogger.Setup(l => l.Log(It.IsAny<string>(), It.IsAny<LogLevel>()));

            var repository = new FileOrderRepository(mockDataSource.Object, mockValidator.Object, mockLogger.Object);
            var result = repository.GetList();

            Assert.Single(result); // Должен вернуть только один валидный заказ
            mockLogger.Verify(l => l.Log(It.Is<string>(msg => msg.Contains("Недействительный заказ")), It.IsAny<LogLevel>()), Times.Once);
        }
    }
}
