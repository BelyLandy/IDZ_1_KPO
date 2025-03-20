using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace ClassLibrary
{
    /// <summary>
    /// Класс для импорта данных из YAML-формата с использованием библиотеки YamlDotNet.
    /// Реализует интерфейс IDataImporter.
    /// </summary>
    public class YamlImporter : IDataImporter
    {
        /// <summary>
        /// Импортирует данные из YAML-файла в FinanceManager.
        /// Очищает существующие данные перед импортом.
        /// </summary>
        /// <param name="manager">Менеджер финансовых данных, в который будут импортированы данные.</param>
        /// <param name="filePath">Путь к файлу, из которого будут импортированы данные.</param>
        public void Import(FinanceManager manager, string filePath)
        {
            // Чтение YAML-файла.
            string yaml = File.ReadAllText(filePath);

            // Конфигурируем десериализатор с соглашением о наименовании (CamelCase).
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            // Десериализация YAML в объект ExportDataModel.
            var data = deserializer.Deserialize<ExportDataModel>(yaml);

            if (data != null)
            {
                // Очистка существующих данных.
                manager.BankAccounts.Clear();
                manager.Categories.Clear();
                manager.Operations.Clear();

                // Добавление импортированных данных.
                manager.BankAccounts.AddRange(data.BankAccounts);
                manager.Categories.AddRange(data.Categories);
                manager.Operations.AddRange(data.Operations);
            }

            // Вывод сообщения об успешном импорте.
            Console.WriteLine("Данные импортированы из YAML файла: " + filePath);
        }
    }
}