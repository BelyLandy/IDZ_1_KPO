using System.Text.Json;

namespace ClassLibrary
{
    /// <summary>
    /// Класс для импорта данных из JSON-формата.
    /// Реализует интерфейс IDataImporter.
    /// </summary>
    public class JsonImporter : IDataImporter
    {
        /// <summary>
        /// Импортирует данные из JSON-файла в FinanceManager.
        /// Очищает существующие данные перед импортом.
        /// </summary>
        /// <param name="manager">Менеджер финансовых данных, в который будут импортированы данные.</param>
        /// <param name="filePath">Путь к файлу, из которого будут импортированы данные.</param>
        public void Import(FinanceManager manager, string filePath)
        {
            // Чтение JSON-файла
            string json = File.ReadAllText(filePath);

            // Десериализация JSON в объект ExportDataModel
            var data = JsonSerializer.Deserialize<ExportDataModel>(json);

            if (data != null)
            {
                // Очистка существующих данных
                manager.BankAccounts.Clear();
                manager.Categories.Clear();
                manager.Operations.Clear();

                // Добавление импортированных данных
                manager.BankAccounts.AddRange(data.BankAccounts);
                manager.Categories.AddRange(data.Categories);
                manager.Operations.AddRange(data.Operations);
            }

            // Вывод сообщения об успешном импорте
            Console.WriteLine("Данные импортированы из JSON файла: " + filePath);
        }
    }
}