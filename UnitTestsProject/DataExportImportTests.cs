using ClassLibrary;
using System.Reflection;
using Xunit;

namespace UnitTestsProject
{
    /// <summary>
    /// Класс для тестирования функциональности экспорта и импорта данных (DataExportImport).
    /// </summary>
    public class DataExportImportTests
    {
        /// <summary>
        /// Тест для проверки, что метод GetExporter возвращает экземпляр CsvExporter для CSV-файлов.
        /// </summary>
        [Fact]
        public void GetExporter_ShouldReturnCsvExporter_ForCsvExtension()
        {
            var manager = new FinanceManager();
            var helpMethods = new HelpMethods();
            var dei = new DataExportImport(manager, helpMethods);
            string csvPath = "data.csv";

            MethodInfo method = typeof(DataExportImport)
                .GetMethod("GetExporter", BindingFlags.NonPublic | BindingFlags.Instance);
            var exporter = method.Invoke(dei, new object[] { csvPath }) as IDataExporter;

            Assert.NotNull(exporter); // Проверяем, что экспортёр не равен null.
            Assert.IsType<CsvExporter>(exporter); // Проверяем, что возвращается экземпляр CsvExporter.
        }

        /// <summary>
        /// Тест для проверки, что метод GetImporter возвращает экземпляр JsonImporter для JSON-файлов.
        /// </summary>
        [Fact]
        public void GetImporter_ShouldReturnJsonImporter_ForJsonExtension()
        {
            var manager = new FinanceManager();
            var helpMethods = new HelpMethods();
            var dei = new DataExportImport(manager, helpMethods);
            string jsonPath = "data.json";

            MethodInfo method = typeof(DataExportImport)
                .GetMethod("GetImporter", BindingFlags.NonPublic | BindingFlags.Instance);
            var importer = method.Invoke(dei, new object[] { jsonPath }) as IDataImporter;

            Assert.NotNull(importer); // Проверяем, что импортёр не равен null.
            Assert.IsType<JsonImporter>(importer); // Проверяем, что возвращается экземпляр JsonImporter.
        }

        /// <summary>
        /// Тест для проверки, что метод GetExporter выбрасывает исключение для неподдерживаемого расширения файла.
        /// </summary>
        [Fact]
        public void GetExporter_ShouldThrowException_ForUnsupportedExtension()
        {
            var manager = new FinanceManager();
            var helpMethods = new HelpMethods();
            var dei = new DataExportImport(manager, helpMethods);
            string unsupportedPath = "data.txt";

            MethodInfo method = typeof(DataExportImport)
                .GetMethod("GetExporter", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.Throws<TargetInvocationException>(() => method.Invoke(dei, new object[] { unsupportedPath })); // Проверяем, что выбрасывается исключение.
        }
    }
}