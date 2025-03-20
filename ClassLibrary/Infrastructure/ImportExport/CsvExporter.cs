namespace ClassLibrary
{
    /// <summary>
    /// Класс для экспорта данных в CSV-формат.
    /// Реализует интерфейс IDataExporter.
    /// </summary>
    public class CsvExporter : IDataExporter
    {
        /// <summary>
        /// Экспортирует данные из FinanceManager в CSV-файл по указанному пути.
        /// </summary>
        /// <param name="manager">Менеджер финансовых данных, содержащий счета, категории и операции.</param>
        /// <param name="filePath">Путь к файлу, в который будут экспортированы данные.</param>
        public void Export(FinanceManager manager, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Экспорт банковских счетов
                writer.WriteLine("BankAccounts:");
                foreach (var account in manager.BankAccounts)
                {
                    writer.WriteLine($"{account.Id},{account.Name},{account.Balance}");
                }
                writer.WriteLine();

                // Экспорт категорий
                writer.WriteLine("Categories:");
                foreach (var category in manager.Categories)
                {
                    writer.WriteLine($"{category.Id},{category.Name},{category.Type}");
                }
                writer.WriteLine();

                // Экспорт операций
                writer.WriteLine("Operations:");
                foreach (var op in manager.Operations)
                {
                    writer.WriteLine($"{op.Id},{op.Type},{op.BankAccountId},{op.Amount},{op.Date:yyyy-MM-dd},{op.Description},{op.CategoryId}");
                }
            }
            Console.WriteLine("Данные экспортированы в CSV файл: " + filePath);
        }
    }
}