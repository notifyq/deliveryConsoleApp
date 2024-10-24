using deliveryClassLibrary.Validation;
using deliveryClassLibrary.Models;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryTests
{
    public class ValidationTests
    {
        [Fact]
        public void IsValid_ShouldReturnFalse_WhenOrderIsNull()
        {
            var validator = new OrderValidation();
            Assert.False(validator.IsValid(null));
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenOrderIdIsInvalid()
        {
            var validator = new OrderValidation();
            var order = new Order { OrderId = 0, Weight = 10, OrderPoint = "Downtown", DeliveryTime = DateTime.Now };
            Assert.False(validator.IsValid(order));
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenWeightIsInvalid()
        {
            var validator = new OrderValidation();
            var order = new Order { OrderId = 1, Weight = 0, OrderPoint = "Downtown", DeliveryTime = DateTime.Now };
            Assert.False(validator.IsValid(order));
        }

        [Fact]
        public void IsValid_ShouldReturnTrue_WhenOrderIsValid()
        {
            var validator = new OrderValidation();
            var order = new Order { OrderId = 1, Weight = 10, OrderPoint = "Downtown", DeliveryTime = DateTime.Now };
            Assert.True(validator.IsValid(order));
        }
    }
}
