using deliveryClassLibrary.Data;
using deliveryClassLibrary.Models;
using deliveryClassLibrary.Repository.Orders;
using deliveryClassLibrary.UserInterface;
using deliveryClassLibrary.Validation;
using deliveryClassLibrary.Logger;
using deliveryClassLibrary.Services;


internal class Program
{
    private static void Main(string[] args)
    {
        string deliveryLog = args.Length > 0 ? args[0] : "log.txt";
        string deliveryOrder = args.Length > 1 ? args[1] : "result.txt";

        var logger = new FileLogger(deliveryLog);
        var dataSource = new FileDataSource<Order>("orders.json", logger);
        var validator = new OrderValidation();
        var orderRepository = new FileOrderRepository(dataSource, validator, logger);
        var orderService = new OrderService(orderRepository, validator, logger);
        var resultWriter = new FileResultWriter(deliveryOrder, logger);

        var userInterface = new ConsoleUserInterface(deliveryLog, deliveryOrder, orderService, logger, resultWriter);
        userInterface.Run();
    }
}