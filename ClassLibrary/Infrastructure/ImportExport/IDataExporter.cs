using ClassLibrary;

/// <summary>
/// Интерфейс для экспорта данных.
/// Определяет метод для экспорта данных из FinanceManager в файл.
/// </summary>
public interface IDataExporter
{
    /// <summary>
    /// Экспортирует данные из FinanceManager в указанный файл.
    /// </summary>
    /// <param name="manager">Менеджер финансовых данных, содержащий счета, категории и операции.</param>
    /// <param name="filePath">Путь к файлу, в который будут экспортированы данные.</param>
    void Export(FinanceManager manager, string filePath);
}