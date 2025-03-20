
namespace ClassLibrary
{
    /// <summary>
    /// Менеджер для хранения и управления доменными объектами.
    /// Соблюдая принцип High Cohesion, он отвечает только за управление данными.
    /// </summary>
    public class FinanceManager
    {
        /// <summary>
        /// Список банковских счетов.
        /// </summary>
        public List<BankAccount> BankAccounts { get; } = new List<BankAccount>();

        /// <summary>
        /// Список категорий.
        /// </summary>
        public List<Category> Categories { get; } = new List<Category>();

        /// <summary>
        /// Список операций.
        /// </summary>
        public List<Operation> Operations { get; } = new List<Operation>();

        // Обновление баланса при создании операции происходит в фасаде (см. ниже)
    }
}
