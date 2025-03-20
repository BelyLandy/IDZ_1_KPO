namespace ClassLibrary.Domain
{
    /// <summary>
    /// Централизованное создание доменных объектов.
    /// Здесь можно добавить общую валидацию и логику создания.
    /// </summary>
    public static class DomainFactory
    {
        /// <summary>
        /// Создает новый банковский счет с указанным именем и начальным балансом.
        /// </summary>
        /// <param name="name">Имя владельца счета. Не может быть пустым.</param>
        /// <param name="initialBalance">Начальный баланс счета. По умолчанию равен 0.</param>
        /// <returns>Созданный банковский счет.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если имя счета пустое или состоит из пробелов.</exception>
        public static BankAccount CreateBankAccount(string name, decimal initialBalance = 0)
        {
            // Пример валидации: название не должно быть пустым
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название счета не может быть пустым");
            return new BankAccount(name, initialBalance);
        }

        /// <summary>
        /// Создает новую категорию с указанным типом и именем.
        /// </summary>
        /// <param name="type">Тип категории (Income или Expense).</param>
        /// <param name="name">Название категории. Не может быть пустым.</param>
        /// <returns>Созданная категория.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если название категории пустое или состоит из пробелов.</exception>
        public static Category CreateCategory(CategoryType type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название категории не может быть пустым");
            return new Category(type, name);
        }

        /// <summary>
        /// Создает новую операцию с указанными параметрами.
        /// </summary>
        /// <param name="type">Тип операции (Income или Expense).</param>
        /// <param name="bankAccountId">Идентификатор банковского счета, связанного с операцией.</param>
        /// <param name="amount">Сумма операции. Для расходов сумма должна быть положительной.</param>
        /// <param name="date">Дата выполнения операции.</param>
        /// <param name="categoryId">Идентификатор категории, связанной с операцией.</param>
        /// <param name="description">Описание операции. По умолчанию пустая строка.</param>
        /// <returns>Созданная операция.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если сумма расхода отрицательная.</exception>
        public static Operation CreateOperation(OperationType type, Guid bankAccountId, decimal amount, DateTime date, Guid categoryId, string description = "")
        {
            // Для расходов сумма должна быть положительной
            if (type == OperationType.Expense && amount < 0)
                throw new ArgumentException("Сумма расхода должна быть положительной");
            return new Operation(type, bankAccountId, amount, date, categoryId, description);
        }
    }
}
