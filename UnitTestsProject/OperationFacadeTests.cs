using ClassLibrary;
using Xunit;

namespace UnitTestsProject
{
    /// <summary>
    /// Класс для тестирования функциональности фасада работы с операциями (OperationFacade).
    /// </summary>
    public class OperationFacadeTests
    {
        /// <summary>
        /// Тест для проверки создания операции.
        /// Убеждаемся, что операция добавляется в список операций и баланс счёта обновляется корректно.
        /// </summary>
        [Fact]
        public void CreateOperation_ShouldAddOperationAndUpdateBalance()
        {
            var manager = new FinanceManager();
            var accountFacade = new AccountFacade(manager);
            var categoryFacade = new CategoryFacade(manager);
            var operationFacade = new OperationFacade(manager);

            // Создаем счет и категории.
            var account = accountFacade.CreateAccount("Account1", 0);
            var incomeCategory = categoryFacade.CreateCategory(CategoryType.Income, "Salary");
            var expenseCategory = categoryFacade.CreateCategory(CategoryType.Expense, "Food");

            var opIncome = operationFacade.CreateOperation(OperationType.Income, account.Id, 1000m, DateTime.Today, incomeCategory.Id, "Salary");
            var opExpense = operationFacade.CreateOperation(OperationType.Expense, account.Id, 300m, DateTime.Today, expenseCategory.Id, "Groceries");

            Assert.Contains(opIncome, manager.Operations); // Проверяем, что операция дохода добавлена.
            Assert.Contains(opExpense, manager.Operations); // Проверяем, что операция расхода добавлена.
            // Ожидаемый баланс: +1000 - 300 = 700.
            Assert.Equal(700m, account.Balance); // Проверяем корректность обновлённого баланса.
        }

        /// <summary>
        /// Тест для проверки редактирования операции.
        /// Убеждаемся, что данные операции обновляются, а баланс счёта пересчитывается корректно.
        /// </summary>
        [Fact]
        public void EditOperation_ShouldUpdateOperationAndRecalculateBalance()
        {
            var manager = new FinanceManager();
            var accountFacade = new AccountFacade(manager);
            var categoryFacade = new CategoryFacade(manager);
            var operationFacade = new OperationFacade(manager);

            var account = accountFacade.CreateAccount("Account1", 0);
            var incomeCategory = categoryFacade.CreateCategory(CategoryType.Income, "Salary");

            var op = operationFacade.CreateOperation(OperationType.Income, account.Id, 1000m, DateTime.Today, incomeCategory.Id, "Salary");
            Assert.Equal(1000m, account.Balance); // Проверяем начальный баланс.

            // Изменяем сумму операции на 1500.
            bool result = operationFacade.EditOperation(op.Id, newType: null, newAmount: 1500m, newDate: null, newCategoryId: null, newDescription: "Updated Salary");

            Assert.True(result); // Проверяем, что редактирование прошло успешно.
            // Баланс должен стать 1500 (удалили старую сумму и добавили новую).
            Assert.Equal(1500m, account.Balance); // Проверяем обновлённый баланс.
            Assert.Equal("Updated Salary", op.Description); // Проверяем обновлённое описание.
        }

        /// <summary>
        /// Тест для проверки удаления операции.
        /// Убеждаемся, что операция удаляется из списка операций, а баланс счёта обновляется корректно.
        /// </summary>
        [Fact]
        public void DeleteOperation_ShouldRemoveOperationAndUpdateBalance()
        {
            var manager = new FinanceManager();
            var accountFacade = new AccountFacade(manager);
            var categoryFacade = new CategoryFacade(manager);
            var operationFacade = new OperationFacade(manager);

            var account = accountFacade.CreateAccount("Account1", 0);
            var incomeCategory = categoryFacade.CreateCategory(CategoryType.Income, "Salary");

            var op = operationFacade.CreateOperation(OperationType.Income, account.Id, 1000m, DateTime.Today, incomeCategory.Id, "Salary");
            Assert.Equal(1000m, account.Balance); // Проверяем начальный баланс.

            bool result = operationFacade.DeleteOperation(op.Id);

            Assert.True(result); // Проверяем, что удаление прошло успешно.
            Assert.DoesNotContain(op, manager.Operations); // Проверяем, что операция удалена из списка.
            // Баланс должен стать 0 после удаления операции.
            Assert.Equal(0m, account.Balance); // Проверяем обновлённый баланс.
        }
    }
}