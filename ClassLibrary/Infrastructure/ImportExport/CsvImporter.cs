namespace ClassLibrary
{
    /// <summary>
    /// Класс для импорта данных из CSV-формата.
    /// Реализует интерфейс IDataImporter.
    /// </summary>
    public class CsvImporter : IDataImporter
    {
        /// <summary>
        /// Импортирует данные из CSV-файла в FinanceManager.
        /// Очищает существующие данные перед импортом.
        /// </summary>
        /// <param name="manager">Менеджер финансовых данных, в который будут импортированы данные.</param>
        /// <param name="filePath">Путь к файлу, из которого будут импортированы данные.</param>
        public void Import(FinanceManager manager, string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int index = 0;

            // Очистка существующих данных
            manager.BankAccounts.Clear();
            manager.Categories.Clear();
            manager.Operations.Clear();

            while (index < lines.Length)
            {
                if (lines[index].StartsWith("BankAccounts:"))
                {
                    index++;
                    // Импорт банковских счетов
                    while (index < lines.Length && !string.IsNullOrWhiteSpace(lines[index]))
                    {
                        string[] parts = lines[index].Split(',');
                        if (parts.Length >= 3)
                        {
                            // Создаем новый объект, игнорируя сохранённый ID (генерируется новый)
                            var account = new BankAccount(parts[1], decimal.Parse(parts[2]));
                            manager.BankAccounts.Add(account);
                        }
                        index++;
                    }
                }
                else if (lines[index].StartsWith("Categories:"))
                {
                    index++;
                    // Импорт категорий
                    while (index < lines.Length && !string.IsNullOrWhiteSpace(lines[index]))
                    {
                        string[] parts = lines[index].Split(',');
                        if (parts.Length >= 3)
                        {
                            CategoryType type = (CategoryType)Enum.Parse(typeof(CategoryType), parts[2]);
                            var category = new Category(type, parts[1]);
                            manager.Categories.Add(category);
                        }
                        index++;
                    }
                }
                else if (lines[index].StartsWith("Operations:"))
                {
                    index++;
                    // Импорт операций
                    while (index < lines.Length && !string.IsNullOrWhiteSpace(lines[index]))
                    {
                        string[] parts = lines[index].Split(',');
                        if (parts.Length >= 7)
                        {
                            OperationType type = (OperationType)Enum.Parse(typeof(OperationType), parts[1]);
                            // Замечание: идентификаторы счёта и категории, считанные из CSV, могут не совпадать с вновь созданными
                            Guid bankAccountId = Guid.NewGuid();
                            decimal amount = decimal.Parse(parts[3]);
                            DateTime date = DateTime.Parse(parts[4]);
                            string description = parts[5];
                            Guid categoryId = Guid.NewGuid();
                            var op = new Operation(type, bankAccountId, amount, date, categoryId, description);
                            manager.Operations.Add(op);
                        }
                        index++;
                    }
                }
                else
                {
                    index++;
                }
            }
            Console.WriteLine("Данные импортированы из CSV файла: " + filePath);
        }
    }
}