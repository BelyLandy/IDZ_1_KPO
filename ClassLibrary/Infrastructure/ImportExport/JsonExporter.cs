using System.Text.Json;

namespace ClassLibrary
{
    /// <summary>
    /// Класс для экспорта данных в JSON-формат.
    /// Реализует интерфейс IDataExporter.
    /// </summary>
    public class JsonExporter : IDataExporter
    {
        /// <summary>
        /// Экспортирует данные из FinanceManager в JSON-файл по указанному пути.
        /// </summary>
        /// <param name="manager">Менеджер финансовых данных, содержащий счета, категории и операции.</param>
        /// <param name="filePath">Путь к файлу, в который будут экспортированы данные.</param>
        public void Export(FinanceManager manager, string filePath)
        {
            // Создание анонимного объекта для сериализации
            var data = new
            {
                BankAccounts = manager.BankAccounts,
                Categories = manager.Categories,
                Operations = manager.Operations
            };

            // Настройки сериализации JSON (с отступами для читаемости)
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Сериализация данных в JSON
            string json = JsonSerializer.Serialize(data, options);

            // Запись JSON в файл
            File.WriteAllText(filePath, json);

            // Вывод сообщения об успешном экспорте
            Console.WriteLine("Данные экспортированы в JSON файл: " + filePath);
        }
    }
}