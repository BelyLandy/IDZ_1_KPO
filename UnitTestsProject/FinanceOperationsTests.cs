using Xunit;
using ClassLibrary;

namespace UnitTestsProject
{
    /// <summary>
    /// Класс для тестирования функциональности, связанной с финансовыми операциями.
    /// </summary>
    public class FinanceOperationsTests
    {
        /// <summary>
        /// Тест для проверки корректности пересчёта балансов счетов.
        /// Создаются счета и операции, после чего проверяется, что баланс каждого счёта обновлён корректно.
        /// </summary>
        [Fact]
        public void RecalculateBalances_ShouldUpdateAccountBalances()
        {
            var manager = new FinanceManager();
            var accountFacade = new AccountFacade(manager);
            var categoryFacade = new CategoryFacade(manager);
            var operationFacade = new OperationFacade(manager);

            // Создаем экземпляр FinanceOperations.
            var financeOps = new FinanceOperations(manager, accountFacade, categoryFacade, operationFacade);

            // Создаем два счета.
            var account1 = accountFacade.CreateAccount("Account1", 0);
            var account2 = accountFacade.CreateAccount("Account2", 0);

            // Создаем категории для доходов и расходов.
            var categoryIncome = categoryFacade.CreateCategory(CategoryType.Income, "Salary");
            var categoryExpense = categoryFacade.CreateCategory(CategoryType.Expense, "Food");

            // Добавляем операции для account1: доход 1000 и расход 200.
            operationFacade.CreateOperation(OperationType.Income, account1.Id, 1000, DateTime.Today, categoryIncome.Id, "Salary");
            operationFacade.CreateOperation(OperationType.Expense, account1.Id, 200, DateTime.Today, categoryExpense.Id, "Groceries");

            // Для account2: только расход 300.
            operationFacade.CreateOperation(OperationType.Expense, account2.Id, 300, DateTime.Today, categoryExpense.Id, "Rent");

            // Пересчитываем балансы.
            financeOps.RecalculateBalances();

            // account1: 1000 - 200 = 800.
            Assert.Equal(800, account1.Balance);
            // account2: -300.
            Assert.Equal(-300, account2.Balance);
        }

        /// <summary>
        /// Тест для проверки, что метод измерения времени пересчёта балансов выполняется без исключений.
        /// </summary>
        [Fact]
        public void MeasureRecalculateBalancesTime_ShouldNotThrowException()
        {
            var manager = new FinanceManager();
            var accountFacade = new AccountFacade(manager);
            var categoryFacade = new CategoryFacade(manager);
            var operationFacade = new OperationFacade(manager);
            var financeOps = new FinanceOperations(manager, accountFacade, categoryFacade, operationFacade);

            var exception = Record.Exception(() => financeOps.MeasureRecalculateBalancesTime());
            Assert.Null(exception);
        }

        /// <summary>
        /// Тест для проверки корректности расчёта разницы между доходами и расходами за заданный период.
        /// Проверяется бизнес-логика с использованием LINQ, аналогично методу DisplayIncomeExpenseDifference.
        /// </summary>
        [Fact]
        public void Analytics_IncomeExpenseDifference_CalculatesCorrectly()
        {
            var manager = new FinanceManager();
            var accountFacade = new AccountFacade(manager);
            var categoryFacade = new CategoryFacade(manager);
            var operationFacade = new OperationFacade(manager);
            var financeOps = new FinanceOperations(manager, accountFacade, categoryFacade, operationFacade);

            // Создаем один счет и две категории.
            var account = accountFacade.CreateAccount("TestAccount", 0);
            var categoryIncome = categoryFacade.CreateCategory(CategoryType.Income, "IncomeCat");
            var categoryExpense = categoryFacade.CreateCategory(CategoryType.Expense, "ExpenseCat");

            // Определяем период.
            DateTime startDate = new DateTime(2023, 1, 1);
            DateTime endDate = new DateTime(2023, 1, 31);

            // Добавляем операции в пределах периода.
            operationFacade.CreateOperation(OperationType.Income, account.Id, 1000, startDate.AddDays(5), categoryIncome.Id, "Income1");
            operationFacade.CreateOperation(OperationType.Expense, account.Id, 300, startDate.AddDays(10), categoryExpense.Id, "Expense1");

            // Бизнес-логика аналитики через LINQ.
            var operationsInPeriod = manager.Operations
                .Where(op => op.Date >= startDate && op.Date <= endDate);
            decimal totalIncome = operationsInPeriod
                .Where(op => op.Type == OperationType.Income)
                .Sum(op => op.Amount);
            decimal totalExpense = operationsInPeriod
                .Where(op => op.Type == OperationType.Expense)
                .Sum(op => op.Amount);
            decimal difference = totalIncome - totalExpense;

            Assert.Equal(1000, totalIncome);
            Assert.Equal(300, totalExpense);
            Assert.Equal(700, difference);
        }
    }
}