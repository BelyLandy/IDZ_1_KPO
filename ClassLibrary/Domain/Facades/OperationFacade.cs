
using ClassLibrary.Domain;

namespace ClassLibrary
{
    /// <summary>
    /// Фасад для работы с операциями.
    /// Здесь дополнительно происходит пересчет баланса счета при создании, редактировании и удалении операций.
    /// </summary>
    public class OperationFacade
    {
        private readonly FinanceManager _manager;

        /// <summary>
        /// Создает экземпляр фасада для работы с операциями.
        /// </summary>
        /// <param name="manager">Экземпляр FinanceManager, используемый для управления операциями и счетами.</param>
        public OperationFacade(FinanceManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Создает новую операцию с указанными параметрами и обновляет баланс связанного счета.
        /// </summary>
        /// <param name="type">Тип операции (Income или Expense).</param>
        /// <param name="bankAccountId">Идентификатор банковского счета, связанного с операцией.</param>
        /// <param name="amount">Сумма операции.</param>
        /// <param name="date">Дата выполнения операции.</param>
        /// <param name="categoryId">Идентификатор категории, связанной с операцией.</param>
        /// <param name="description">Описание операции. По умолчанию пустая строка.</param>
        /// <returns>Созданная операция.</returns>
        public Operation CreateOperation(OperationType type, Guid bankAccountId, decimal amount, DateTime date, Guid categoryId, string description = "")
        {
            var operation = DomainFactory.CreateOperation(type, bankAccountId, amount, date, categoryId, description);
            _manager.Operations.Add(operation);

            var account = _manager.BankAccounts.FirstOrDefault(a => a.Id == bankAccountId);
            if (account != null)
            {
                account.Balance += (type == OperationType.Income ? amount : -amount);
            }
            return operation;
        }

        /// <summary>
        /// Редактирует существующую операцию, обновляя её параметры и пересчитывая баланс связанного счета.
        /// </summary>
        /// <param name="id">Идентификатор операции, которую нужно отредактировать.</param>
        /// <param name="newType">Новый тип операции. Если не указан, тип остается неизменным.</param>
        /// <param name="newAmount">Новая сумма операции. Если не указана, сумма остается неизменной.</param>
        /// <param name="newDate">Новая дата операции. Если не указана, дата остается неизменной.</param>
        /// <param name="newCategoryId">Новый идентификатор категории. Если не указан, категория остается неизменной.</param>
        /// <param name="newDescription">Новое описание операции. Если не указано, описание остается неизменным.</param>
        /// <returns>True, если операция была успешно отредактирована, иначе False.</returns>
        public bool EditOperation(Guid id, OperationType? newType = null, decimal? newAmount = null, DateTime? newDate = null, Guid? newCategoryId = null, string newDescription = null)
        {
            var op = _manager.Operations.FirstOrDefault(o => o.Id == id);
            if (op == null) return false;

            var account = _manager.BankAccounts.FirstOrDefault(a => a.Id == op.BankAccountId);
            if (account != null)
            {
                // Откат предыдущей операции
                account.Balance += (op.Type == OperationType.Income ? -op.Amount : op.Amount);
            }

            if (newType.HasValue)
                op.Type = newType.Value;
            if (newAmount.HasValue)
                op.Amount = newAmount.Value;
            if (newDate.HasValue)
                op.Date = newDate.Value;
            if (newCategoryId.HasValue)
                op.CategoryId = newCategoryId.Value;
            if (newDescription != null)
                op.Description = newDescription;

            if (account != null)
            {
                // Применение обновлённой операции
                account.Balance += (op.Type == OperationType.Income ? op.Amount : -op.Amount);
            }
            return true;
        }

        /// <summary>
        /// Удаляет операцию по указанному идентификатору и корректирует баланс связанного счета.
        /// </summary>
        /// <param name="id">Идентификатор операции, которую нужно удалить.</param>
        /// <returns>True, если операция была успешно удалена, иначе False.</returns>
        public bool DeleteOperation(Guid id)
        {
            var op = _manager.Operations.FirstOrDefault(o => o.Id == id);
            if (op == null) return false;

            var account = _manager.BankAccounts.FirstOrDefault(a => a.Id == op.BankAccountId);
            if (account != null)
            {
                // Отмена операции из баланса
                account.Balance += (op.Type == OperationType.Income ? -op.Amount : op.Amount);
            }
            _manager.Operations.Remove(op);
            return true;
        }
    }
}
