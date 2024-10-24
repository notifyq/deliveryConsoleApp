using deliveryClassLibrary.Data;
using deliveryClassLibrary.Logger;
using deliveryClassLibrary.Models;
using deliveryClassLibrary.Repository.Orders;
using deliveryClassLibrary.Services;
using deliveryClassLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.UserInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        private readonly string _deliveryLog;
        private readonly string _deliveryOrder;
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;
        private readonly IResultWriter _resultWriter;

        public ConsoleUserInterface(string deliveryLog, string deliveryOrder, IOrderService orderService, ILogger logger, IResultWriter resultWriter)
        {
            _deliveryLog = deliveryLog;
            _deliveryOrder = deliveryOrder;
            _orderService = orderService;
            _logger = logger;
            _resultWriter = resultWriter;
        }

        public string ReadMessage(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void Run()
        {

            _logger.Log("Запуск программы");
            _logger.Log($"Лог-файл: {_deliveryLog}");
            _logger.Log($"Файл результата: {_deliveryOrder}");

            WriteMessage("Хотите сгенерировать случайный список заказов в файле orders.json? (y/n)");

            string generateInput = ReadMessage("Ввод: ");
            if (generateInput.ToLower() == "y")
            {   

                string countInput = ReadMessage("Введите количество заказов для генерации: ");
                int count = int.TryParse(countInput, out int parseCount) ? parseCount : 50;
                _orderService.GenerateAndSaveOrders(count);

                WriteMessage("Список заказов успешно сгенерирован.");
                WriteMessage($"Для генерации используются районы:" +
                    $"\n\"Центральный\",\r\n" +
                    $"\"Калининский\",\r\n" +
                    $"\"Октябрьский\",\r\n" +
                    $"\"Советский\",\r\n" +
                    $"\"Демский\",\r\n" +
                    $"\"Инорс\"\n");
            }

            WriteMessage("\nВведите район доставки:");
            string district = ReadMessage("Район: ");

            WriteMessage("Введите время первой доставки (формат: yyyy-MM-dd HH:mm:ss):");
            string deliveryTimeInput = ReadMessage("Время: ");
            if (!DateTime.TryParse(deliveryTimeInput, out var deliveryTime))
            {
                WriteMessage("Некорректный формат даты: yyyy-MM-dd HH:mm:ss.");
                return;
            }
            var filteredOrders = _orderService.GetFilteredOrders(district, deliveryTime);

            if (filteredOrders.Count == 0)
            {
                WriteMessage("Не найдено ни одного подходящего заказа.");
            }
            else
            {
                _resultWriter.WriteResult(filteredOrders);
                WriteMessage($"Количество найденных заказов: {filteredOrders.Count}");

                WriteMessage($"Результат сохранён в файл: {_deliveryOrder}");

            }

            _logger.Log("Завершение программы");
        }
    }
}
