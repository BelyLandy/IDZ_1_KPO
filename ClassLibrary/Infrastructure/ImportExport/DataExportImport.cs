namespace ClassLibrary
{
    /// <summary>
    /// Класс для управления импортом и экспортом данных.
    /// Использует паттерн Фабрика для выбора нужного экспортёра/импортёра в зависимости от расширения файла.
    /// </summary>
    public class DataExportImport
    {
        private readonly FinanceManager _financeManager;
        private readonly HelpMethods _helpMethods;

        /// <summary>
        /// Создает экземпляр класса DataExportImport.
        /// </summary>
        /// <param name="financeManager">Менеджер финансовых данных, с которым будет работать экспорт/импорт.</param>
        /// <param name="helpMethods">Вспомогательные методы для получения путей к файлам.</param>
        public DataExportImport(FinanceManager financeManager, HelpMethods helpMethods)
        {
            _financeManager = financeManager;
            _helpMethods = helpMethods;
        }

        /// <summary>
        /// Экспортирует все данные из FinanceManager в файл.
        /// </summary>
        public void ExportAllData()
        {
            // Получаем путь для сохранения через HelpMethods
            string? filePath = _helpMethods.GetFilePath("save");
            if (filePath == null) return;

            IDataExporter exporter = GetExporter(filePath);
            exporter.Export(_financeManager, filePath);
        }

        /// <summary>
        /// Импортирует данные из файла в FinanceManager.
        /// </summary>
        public void ImportData()
        {
            // Получаем путь для чтения
            string? filePath = _helpMethods.GetFilePath("read");
            if (filePath == null) return;

            IDataImporter importer = GetImporter(filePath);
            importer.Import(_financeManager, filePath);
        }

        /// <summary>
        /// Возвращает экземпляр экспортёра в зависимости от расширения файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу, по которому определяется формат экспорта.</param>
        /// <returns>Экземпляр класса, реализующего интерфейс IDataExporter.</returns>
        /// <exception cref="NotSupportedException">Выбрасывается, если формат файла не поддерживается.</exception>
        private IDataExporter GetExporter(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension switch
            {
                ".csv" => new CsvExporter(),
                ".json" => new JsonExporter(),
                ".yaml" or ".yml" => new YamlExporter(),
                _ => throw new NotSupportedException("Неподдерживаемый формат экспорта.")
            };
        }

        /// <summary>
        /// Возвращает экземпляр импортёра в зависимости от расширения файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу, по которому определяется формат импорта.</param>
        /// <returns>Экземпляр класса, реализующего интерфейс IDataImporter.</returns>
        /// <exception cref="NotSupportedException">Выбрасывается, если формат файла не поддерживается.</exception>
        private IDataImporter GetImporter(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension switch
            {
                ".csv" => new CsvImporter(),
                ".json" => new JsonImporter(),
                ".yaml" or ".yml" => new YamlImporter(),
                _ => throw new NotSupportedException("Неподдерживаемый формат импорта.")
            };
        }
    }
}