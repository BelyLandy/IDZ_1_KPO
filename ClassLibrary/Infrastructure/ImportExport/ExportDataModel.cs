namespace ClassLibrary
{
    /// <summary>
    /// Модель для экспорта и импорта данных.
    /// Содержит списки банковских счетов, категорий и операций.
    /// </summary>
    public class ExportDataModel
    {
        /// <summary>
        /// Список банковских счетов.
        /// </summary>
        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

        /// <summary>
        /// Список категорий.
        /// </summary>
        public List<Category> Categories { get; set; } = new List<Category>();

        /// <summary>
        /// Список операций.
        /// </summary>
        public List<Operation> Operations { get; set; } = new List<Operation>();
    }
}