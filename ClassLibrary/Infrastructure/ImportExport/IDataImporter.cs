namespace ClassLibrary
{
    /// <summary>
    /// Интерфейс для импорта данных.
    /// Определяет метод для импорта данных из файла в FinanceManager.
    /// </summary>
    public interface IDataImporter
    {
        /// <summary>
        /// Импортирует данные из указанного файла в FinanceManager.
        /// </summary>
        /// <param name="manager">Менеджер финансовых данных, в который будут импортированы данные.</param>
        /// <param name="filePath">Путь к файлу, из которого будут импортированы данные.</param>
        void Import(FinanceManager manager, string filePath);
    }
}