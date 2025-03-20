
using ClassLibrary.Domain;

namespace ClassLibrary
{
    /// <summary>
    /// Фасад для работы со счетами.
    /// </summary>
    public class AccountFacade
    {
        private readonly FinanceManager _manager;

        /// <summary>
        /// Создает экземпляр фасада для работы со счетами.
        /// </summary>
        /// <param name="manager">Экземпляр FinanceManager, используемый для управления счетами.</param>
        public AccountFacade(FinanceManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Создает новый банковский счет с указанным именем и начальным балансом.
        /// </summary>
        /// <param name="name">Имя владельца счета.</param>
        /// <param name="initialBalance">Начальный баланс счета. По умолчанию равен 0.</param>
        /// <returns>Созданный банковский счет.</returns>
        public BankAccount CreateAccount(string name, decimal initialBalance = 0)
        {
            var account = DomainFactory.CreateBankAccount(name, initialBalance);
            _manager.BankAccounts.Add(account);
            return account;
        }

        /// <summary>
        /// Редактирует существующий банковский счет, изменяя его имя и/или баланс.
        /// </summary>
        /// <param name="id">Идентификатор счета, который нужно отредактировать.</param>
        /// <param name="newName">Новое имя владельца счета.</param>
        /// <param name="newBalance">Новый баланс счета. Если не указан, баланс остается неизменным.</param>
        /// <returns>True, если счет был успешно отредактирован, иначе False.</returns>
        public bool EditAccount(Guid id, string newName, decimal? newBalance = null)
        {
            var account = _manager.BankAccounts.FirstOrDefault(a => a.Id == id);
            if (account == null) return false;
            account.Name = newName;
            if (newBalance.HasValue)
                account.Balance = newBalance.Value;
            return true;
        }

        /// <summary>
        /// Удаляет банковский счет по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор счета, который нужно удалить.</param>
        /// <returns>True, если счет был успешно удален, иначе False.</returns>
        public bool DeleteAccount(Guid id)
        {
            var account = _manager.BankAccounts.FirstOrDefault(a => a.Id == id);
            if (account == null) return false;
            _manager.BankAccounts.Remove(account);
            return true;
        }
    }
}
