using deliveryClassLibrary.Logger;
using deliveryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Data
{
    /// <summary>
    /// Запись результата фильтрации в файл, необходим для обработки исключений вызванных при записи
    /// </summary>
    public class FileResultWriter : IResultWriter
    {
        private readonly string _filePath;
        private readonly ILogger _logger;

        public FileResultWriter(string filePath, ILogger logger)
        {
            _filePath = filePath;
            _logger = logger;
        }

        public void WriteResult(IEnumerable<Order> orders)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_filePath))
                {
                    if (orders == null || !orders.Any())
                    {
                        throw new InvalidOperationException("Не найдено ни одного подходящего заказа.");
                    }

                    foreach (var order in orders)
                    {
                        string orderDetails = $"ID: {order.OrderId}, Вес: {order.Weight}, Район: {order.OrderPoint}, Время доставки: {order.DeliveryTime}";
                        sw.WriteLine(orderDetails);
                    }
                }
                _logger.Log($"Результат записан в файл {_filePath}");
            }
            catch (IOException ex)
            {
                _logger.Log($"Ошибка записи результата фильтрации в файл: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Log($"Неизвестная ошибка: {ex.Message}");
            }
        }
    }
}
