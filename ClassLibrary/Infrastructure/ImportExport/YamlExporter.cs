using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ClassLibrary
{
    /// <summary>
    /// Класс для экспорта данных в YAML-формат с использованием библиотеки YamlDotNet.
    /// Реализует интерфейс IDataExporter.
    /// </summary>
    public class YamlExporter : IDataExporter
    {
        /// <summary>
        /// Экспортирует данные из FinanceManager в YAML-файл по указанному пути.
        /// </summary>
        /// <param name="manager">Менеджер финансовых данных, содержащий счета, категории и операции.</param>
        /// <param name="filePath">Путь к файлу, в который будут экспортированы данные.</param>
        public void Export(FinanceManager manager, string filePath)
        {
            // Создаём объект для экспорта данных.
            var data = new ExportDataModel
            {
                BankAccounts = manager.BankAccounts,
                Categories = manager.Categories,
                Operations = manager.Operations
            };

            // Конфигурируем сериализатор с соглашением о наименовании (CamelCase).
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            // Сериализация данных в YAML.
            string yaml = serializer.Serialize(data);

            // Запись YAML в файл.
            File.WriteAllText(filePath, yaml);

            // Вывод сообщения об успешном экспорте.
            Console.WriteLine("Данные экспортированы в YAML файл: " + filePath);
        }
    }
}