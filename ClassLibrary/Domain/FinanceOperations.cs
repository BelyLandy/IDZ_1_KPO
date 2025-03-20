
namespace ClassLibrary
{
    /// <summary>
    /// Класс, инкапсулирующий логику работы с доменной моделью финансов.
    /// Предоставляет методы для создания, редактирования, удаления и отображения данных,
    /// а также для выполнения аналитических операций.
    /// </summary>
    public class FinanceOperations
    {
        private readonly FinanceManager _financeManager;
        private readonly AccountFacade _accountFacade;
        private readonly CategoryFacade _categoryFacade;
        private readonly OperationFacade _operationFacade;

        // Ссылка на вспомогательный класс для красивого ввода/вывода.
        private readonly HelpMethods _helpMethods;

        /// <summary>
        /// Создает экземпляр класса FinanceOperations.
        /// </summary>
        /// <param name="financeManager">Менеджер для управления финансовыми данными.</param>
        /// <param name="accountFacade">Фасад для работы со счетами.</param>
        /// <param name="categoryFacade">Фасад для работы с категориями.</param>
        /// <param name="operationFacade">Фасад для работы с операциями.</param>
        public FinanceOperations(FinanceManager financeManager,
                                 AccountFacade accountFacade,
                                 CategoryFacade categoryFacade,
                                 OperationFacade operationFacade)
        {
            _financeManager = financeManager;
            _accountFacade = accountFacade;
            _categoryFacade = categoryFacade;
            _operationFacade = operationFacade;

            // Инициализируем HelpMethods (либо можно передать извне в конструкторе)
            _helpMethods = new HelpMethods();
        }

        /// <summary>
        /// Создает новый банковский счет на основе введенных пользователем данных.
        /// </summary>
        public void CreateAccount()
        {
            _helpMethods.NiceOutput("", clear: true); // Очищаем консоль

            string name = _helpMethods.GetString("Введите название счета: ");
            string balanceStr = _helpMethods.GetString("Введите начальный баланс: ");
            decimal balance = decimal.Parse(balanceStr);

            var account = _accountFacade.CreateAccount(name, balance);
            _helpMethods.NiceOutput($"Создан счет: {account}", ConsoleColor.Green);
        }

        /// <summary>
        /// Редактирует существующий банковский счет на основе введенных пользователем данных.
        /// </summary>
        public void EditAccount()
        {
            _helpMethods.NiceOutput("", clear: true);

            string idStr = _helpMethods.GetString("Введите Id счета для редактирования: ");
            Guid id = Guid.Parse(idStr);

            string newName = _helpMethods.GetString("Введите новое название: ");
            string balanceInput = _helpMethods.GetString("Введите новый баланс (оставьте пустым, если не менять): ");

            decimal? newBalance = string.IsNullOrWhiteSpace(balanceInput)
                ? (decimal?)null
                : decimal.Parse(balanceInput);

            bool result = _accountFacade.EditAccount(id, newName, newBalance);

            if (result)
                _helpMethods.NiceOutput("Счет обновлён.", ConsoleColor.Green);
            else
                _helpMethods.NiceOutput("Счет не найден.", ConsoleColor.Red);
        }

        /// <summary>
        /// Удаляет банковский счет по указанному идентификатору.
        /// </summary>
        public void DeleteAccount()
        {
            _helpMethods.NiceOutput("", clear: true);

            string idStr = _helpMethods.GetString("Введите Id счета для удаления: ");
            Guid id = Guid.Parse(idStr);

            bool result = _accountFacade.DeleteAccount(id);

            if (result)
                _helpMethods.NiceOutput("Счет удалён.", ConsoleColor.Green);
            else
                _helpMethods.NiceOutput("Счет не найден.", ConsoleColor.Red);
        }

        /// <summary>
        /// Создает новую категорию на основе введенных пользователем данных.
        /// </summary>
        public void CreateCategory()
        {
            _helpMethods.NiceOutput("", clear: true);

            string name = _helpMethods.GetString("Введите название категории: ");
            string typeInput = _helpMethods.GetString("Введите тип (Income/Expense): ");
            CategoryType type = (CategoryType)Enum.Parse(typeof(CategoryType), typeInput, true);

            var category = _categoryFacade.CreateCategory(type, name);
            _helpMethods.NiceOutput($"Создана категория: {category}", ConsoleColor.Green);
        }

        /// <summary>
        /// Редактирует существующую категорию на основе введенных пользователем данных.
        /// </summary>
        public void EditCategory()
        {
            _helpMethods.NiceOutput("", clear: true);

            string idStr = _helpMethods.GetString("Введите Id категории для редактирования: ");
            Guid id = Guid.Parse(idStr);

            string newName = _helpMethods.GetString("Введите новое название: ");
            string typeInput = _helpMethods.GetString("Введите новый тип (Income/Expense) или оставьте пустым: ");
            CategoryType? newType = string.IsNullOrWhiteSpace(typeInput)
                ? (CategoryType?)null
                : (CategoryType)Enum.Parse(typeof(CategoryType), typeInput, true);

            bool result = _categoryFacade.EditCategory(id, newName, newType);

            if (result)
                _helpMethods.NiceOutput("Категория обновлена.", ConsoleColor.Green);
            else
                _helpMethods.NiceOutput("Категория не найдена.", ConsoleColor.Red);
        }

        /// <summary>
        /// Удаляет категорию по указанному идентификатору.
        /// </summary>
        public void DeleteCategory()
        {
            _helpMethods.NiceOutput("", clear: true);

            string idStr = _helpMethods.GetString("Введите Id категории для удаления: ");
            Guid id = Guid.Parse(idStr);

            bool result = _categoryFacade.DeleteCategory(id);

            if (result)
                _helpMethods.NiceOutput("Категория удалена.", ConsoleColor.Green);
            else
                _helpMethods.NiceOutput("Категория не найдена.", ConsoleColor.Red);
        }

        /// <summary>
        /// Создает новую операцию на основе введенных пользователем данных.
        /// </summary>
        public void CreateOperation()
        {
            _helpMethods.NiceOutput("", clear: true);

            string accountIdStr = _helpMethods.GetString("Введите Id счета для операции: ");
            Guid accountId = Guid.Parse(accountIdStr);

            string typeInput = _helpMethods.GetString("Введите тип операции (Income/Expense): ");
            OperationType opType = (OperationType)Enum.Parse(typeof(OperationType), typeInput, true);

            string amountStr = _helpMethods.GetString("Введите сумму: ");
            decimal amount = decimal.Parse(amountStr);

            string dateStr = _helpMethods.GetString("Введите дату (yyyy-MM-dd): ");
            DateTime date = DateTime.Parse(dateStr);

            string categoryIdStr = _helpMethods.GetString("Введите Id категории: ");
            Guid categoryId = Guid.Parse(categoryIdStr);

            string description = _helpMethods.GetString("Введите описание (опционально): ", _color: ConsoleColor.Yellow, readColor: ConsoleColor.Green);

            try
            {
                var op = _operationFacade.CreateOperation(opType, accountId, amount, date, categoryId, description);
                _helpMethods.NiceOutput($"Создана операция: {op}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                _helpMethods.NiceOutput($"Ошибка создания операции: {ex.Message}", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Редактирует существующую операцию на основе введенных пользователем данных.
        /// </summary>
        public void EditOperation()
        {
            _helpMethods.NiceOutput("", clear: true);

            string idStr = _helpMethods.GetString("Введите Id операции для редактирования: ");
            Guid id = Guid.Parse(idStr);

            string typeInput = _helpMethods.GetString("Введите новый тип (Income/Expense) или оставьте пустым: ");
            OperationType? newType = string.IsNullOrWhiteSpace(typeInput)
                ? (OperationType?)null
                : (OperationType)Enum.Parse(typeof(OperationType), typeInput, true);

            string amountInput = _helpMethods.GetString("Введите новую сумму или оставьте пустым: ");
            decimal? newAmount = string.IsNullOrWhiteSpace(amountInput)
                ? (decimal?)null
                : decimal.Parse(amountInput);

            string dateInput = _helpMethods.GetString("Введите новую дату (yyyy-MM-dd) или оставьте пустым: ");
            DateTime? newDate = string.IsNullOrWhiteSpace(dateInput)
                ? (DateTime?)null
                : DateTime.Parse(dateInput);

            string catInput = _helpMethods.GetString("Введите новый Id категории или оставьте пустым: ");
            Guid? newCategoryId = string.IsNullOrWhiteSpace(catInput)
                ? (Guid?)null
                : Guid.Parse(catInput);

            string newDescription = _helpMethods.GetString("Введите новое описание или оставьте пустым: ", _color: ConsoleColor.Yellow, readColor: ConsoleColor.Green);

            bool result = _operationFacade.EditOperation(id, newType, newAmount, newDate, newCategoryId, newDescription);

            if (result)
                _helpMethods.NiceOutput("Операция обновлена.", ConsoleColor.Green);
            else
                _helpMethods.NiceOutput("Операция не найдена.", ConsoleColor.Red);
        }

        /// <summary>
        /// Удаляет операцию по указанному идентификатору.
        /// </summary>
        public void DeleteOperation()
        {
            _helpMethods.NiceOutput("", clear: true);

            string idStr = _helpMethods.GetString("Введите Id операции для удаления: ");
            Guid id = Guid.Parse(idStr);

            bool result = _operationFacade.DeleteOperation(id);

            if (result)
                _helpMethods.NiceOutput("Операция удалена.", ConsoleColor.Green);
            else
                _helpMethods.NiceOutput("Операция не найдена.", ConsoleColor.Red);
        }

        /// <summary>
        /// Отображает все данные: счета, категории и операции.
        /// </summary>
        public void ShowData()
        {
            _helpMethods.NiceOutput("", clear: true);

            if (_financeManager.BankAccounts.Count != 0)
            {
                _helpMethods.NiceOutput("Счета:", ConsoleColor.Cyan, title: 1, speed: 0);
            }
            foreach (var acc in _financeManager.BankAccounts)
            {
                _helpMethods.NiceOutput(acc.ToString(), ConsoleColor.White);
            }

            if (_financeManager.Categories.Count != 0)
            {
                _helpMethods.NiceOutput("\nКатегории:", ConsoleColor.Cyan, title: 1, speed: 0);
            }
            foreach (var cat in _financeManager.Categories)
            {
                _helpMethods.NiceOutput(cat.ToString(), ConsoleColor.White);
            }

            if (_financeManager.Operations.Count != 0)
            {
                _helpMethods.NiceOutput("\nОперации:", ConsoleColor.Cyan, title: 1, speed: 0);
            }
            foreach (var op in _financeManager.Operations)
            {
                _helpMethods.NiceOutput(op.ToString(), ConsoleColor.White);
            }

            if (_financeManager.BankAccounts.Count == 0
                && _financeManager.Categories.Count == 0
                && _financeManager.Operations.Count == 0)
            {
                _helpMethods.NiceOutput("Никаких данных еще нет!", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Подсчитывает разницу между доходами и расходами за выбранный период.
        /// </summary>
        public void DisplayIncomeExpenseDifference()
        {
            _helpMethods.NiceOutput("", clear: true);

            try
            {
                string startStr = _helpMethods.GetString("Введите дату начала периода (yyyy-MM-dd): ");
                DateTime startDate = DateTime.Parse(startStr);

                string endStr = _helpMethods.GetString("Введите дату окончания периода (yyyy-MM-dd): ");
                DateTime endDate = DateTime.Parse(endStr);

                var operationsInPeriod = _financeManager.Operations
                    .Where(op => op.Date >= startDate && op.Date <= endDate);

                decimal totalIncome = operationsInPeriod
                    .Where(op => op.Type == OperationType.Income)
                    .Sum(op => op.Amount);

                decimal totalExpense = operationsInPeriod
                    .Where(op => op.Type == OperationType.Expense)
                    .Sum(op => op.Amount);

                _helpMethods.NiceOutput($"\nЗа период с {startDate:yyyy-MM-dd} по {endDate:yyyy-MM-dd}:", ConsoleColor.Cyan);
                _helpMethods.NiceOutput($"Общий доход: {totalIncome}", ConsoleColor.White);
                _helpMethods.NiceOutput($"Общий расход: {totalExpense}", ConsoleColor.White);
                _helpMethods.NiceOutput($"Разница (Доход - Расход): {totalIncome - totalExpense}", ConsoleColor.Yellow);
            }
            catch (Exception ex)
            {
                _helpMethods.NiceOutput($"Ошибка при расчёте аналитики: {ex.Message}", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Группирует доходы и расходы по категориям за выбранный период.
        /// </summary>
        public void DisplayOperationsGroupedByCategory()
        {
            _helpMethods.NiceOutput("", clear: true);

            try
            {
                string startStr = _helpMethods.GetString("Введите дату начала периода (yyyy-MM-dd): ");
                DateTime startDate = DateTime.Parse(startStr);

                string endStr = _helpMethods.GetString("Введите дату окончания периода (yyyy-MM-dd): ");
                DateTime endDate = DateTime.Parse(endStr);

                var operationsInPeriod = _financeManager.Operations
                    .Where(op => op.Date >= startDate && op.Date <= endDate)
                    .GroupBy(op => op.CategoryId);

                _helpMethods.NiceOutput($"\nГруппировка операций по категориям за период с {startDate:yyyy-MM-dd} по {endDate:yyyy-MM-dd}:", ConsoleColor.Cyan);

                foreach (var group in operationsInPeriod)
                {
                    var category = _financeManager.Categories.FirstOrDefault(c => c.Id == group.Key);
                    string categoryName = category != null ? category.Name : "Неизвестная категория";
                    decimal total = group.Sum(op => op.Amount * (op.Type == OperationType.Income ? 1 : -1));

                    _helpMethods.NiceOutput($"Категория: {categoryName}, Суммарное значение: {total}", ConsoleColor.White);
                }
            }
            catch (Exception ex)
            {
                _helpMethods.NiceOutput($"Ошибка при группировке операций: {ex.Message}", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Дополнительная аналитика: процентное соотношение доходов и расходов за выбранный период.
        /// </summary>
        public void DisplayAdditionalAnalytics()
        {
            _helpMethods.NiceOutput("", clear: true);

            try
            {
                string startStr = _helpMethods.GetString("Введите дату начала периода (yyyy-MM-dd): ");
                DateTime startDate = DateTime.Parse(startStr);

                string endStr = _helpMethods.GetString("Введите дату окончания периода (yyyy-MM-dd): ");
                DateTime endDate = DateTime.Parse(endStr);

                var operationsInPeriod = _financeManager.Operations
                    .Where(op => op.Date >= startDate && op.Date <= endDate);

                decimal totalIncome = operationsInPeriod
                    .Where(op => op.Type == OperationType.Income)
                    .Sum(op => op.Amount);

                decimal totalExpense = operationsInPeriod
                    .Where(op => op.Type == OperationType.Expense)
                    .Sum(op => op.Amount);

                decimal total = totalIncome + totalExpense;

                if (total == 0)
                {
                    _helpMethods.NiceOutput("За выбранный период не зарегистрировано операций.", ConsoleColor.Yellow);
                }
                else
                {
                    double incomePercentage = (double)(totalIncome / total) * 100;
                    double expensePercentage = (double)(totalExpense / total) * 100;

                    _helpMethods.NiceOutput($"\nЗа период с {startDate:yyyy-MM-dd} по {endDate:yyyy-MM-dd}:", ConsoleColor.Cyan);
                    _helpMethods.NiceOutput($"Доходы составляют {incomePercentage:F2}% от общего объёма операций.", ConsoleColor.White);
                    _helpMethods.NiceOutput($"Расходы составляют {expensePercentage:F2}% от общего объёма операций.", ConsoleColor.White);
                }
            }
            catch (Exception ex)
            {
                _helpMethods.NiceOutput($"Ошибка при дополнительной аналитике: {ex.Message}", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Пересчитывает баланс для каждого счета на основе суммарных операций.
        /// </summary>
        public void RecalculateBalances()
        {
            _helpMethods.NiceOutput("", clear: true);

            try
            {
                _helpMethods.NiceOutput("Начинается пересчёт балансов счетов...", ConsoleColor.Cyan);

                foreach (var account in _financeManager.BankAccounts)
                {
                    decimal computedBalance = _financeManager.Operations
                        .Where(op => op.BankAccountId == account.Id)
                        .Sum(op => op.Type == OperationType.Income ? op.Amount : -op.Amount);

                    account.Balance = computedBalance;
                    _helpMethods.NiceOutput($"Счет '{account.Name}' обновлён: новый баланс = {computedBalance}", ConsoleColor.Green);
                }

                _helpMethods.NiceOutput("\nПересчёт балансов завершён.", ConsoleColor.Cyan);
            }
            catch (Exception ex)
            {
                _helpMethods.NiceOutput($"Ошибка при пересчёте балансов: {ex.Message}", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Измеряет время выполнения сценария пересчёта балансов.
        /// </summary>
        public void MeasureRecalculateBalancesTime()
        {
            // Оборачиваем метод RecalculateBalances в команду.
            ICommand command = new SimpleCommand(() => RecalculateBalances());
            ICommand timedCommand = new CommandTimeDecorator(command);
            timedCommand.Execute();
        }
    }
}
