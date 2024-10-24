using deliveryClassLibrary.Logger;
using deliveryClassLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deliveryClassLibrary.Data
{
    // Паттерн "Адаптер"

    /// <summary>
    /// Получение данных из JSON файла
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FileDataSource<T> : IDataSource<T>
    {
        private readonly string _filePath;
        private readonly ILogger _logger;

        public FileDataSource(string filePath, ILogger logger)
        {
            _filePath = filePath;
            _logger = logger;
        }

        public IEnumerable<T> GetAll()
        {
            var items = new List<T>();
            try
            {
                var json = File.ReadAllText(_filePath);
                items = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (FileNotFoundException)
            {
                _logger.Log("Файл с данными не найден.", LogLevel.Error);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                _logger.Log($"Ошибка при парсинге: {ex.Message}", LogLevel.Error);
            }

            _logger.Log($"Успешное получение данных из файла {_filePath}");
            return items;
        }

        public void Save(IEnumerable<T> items)
        {
            var json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

    }
}
