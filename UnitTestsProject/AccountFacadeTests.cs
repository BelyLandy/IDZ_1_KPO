using ClassLibrary;
using Xunit;

namespace UnitTestsProject
{
    /// <summary>
    /// Класс для тестирования функциональности фасада работы со счетами (AccountFacade).
    /// </summary>
    public class AccountFacadeTests
    {
        /// <summary>
        /// Тест для проверки создания счёта.
        /// Убеждаемся, что счёт добавляется в список счетов и его данные корректны.
        /// </summary>
        [Fact]
        public void CreateAccount_ShouldAddAccount()
        {
            var manager = new FinanceManager();
            var facade = new AccountFacade(manager);
            string name = "Test Account";
            decimal initialBalance = 100m;

            var account = facade.CreateAccount(name, initialBalance);

            Assert.Contains(account, manager.BankAccounts); // Проверяем, что счёт добавлен в список.
            Assert.Equal(name, account.Name); // Проверяем корректность имени.
            Assert.Equal(initialBalance, account.Balance); // Проверяем корректность баланса.
        }

        /// <summary>
        /// Тест для проверки редактирования счёта.
        /// Убеждаемся, что данные счёта обновляются корректно.
        /// </summary>
        [Fact]
        public void EditAccount_ShouldUpdateAccount()
        {
            var manager = new FinanceManager();
            var facade = new AccountFacade(manager);
            var account = facade.CreateAccount("Original Account", 50m);
            string newName = "Updated Account";
            decimal newBalance = 200m;

            bool result = facade.EditAccount(account.Id, newName, newBalance);

            Assert.True(result); // Проверяем, что редактирование прошло успешно.
            Assert.Equal(newName, account.Name); // Проверяем обновлённое имя.
            Assert.Equal(newBalance, account.Balance); // Проверяем обновлённый баланс.
        }

        /// <summary>
        /// Тест для проверки редактирования несуществующего счёта.
        /// Убеждаемся, что метод возвращает false, если счёт не найден.
        /// </summary>
        [Fact]
        public void EditAccount_NonExisting_ShouldReturnFalse()
        {
            var manager = new FinanceManager();
            var facade = new AccountFacade(manager);
            Guid fakeId = Guid.NewGuid(); // Создаём несуществующий ID.

            bool result = facade.EditAccount(fakeId, "New Name", 100m);

            Assert.False(result); // Проверяем, что редактирование не удалось.
        }

        /// <summary>
        /// Тест для проверки удаления счёта.
        /// Убеждаемся, что счёт удаляется из списка счетов.
        /// </summary>
        [Fact]
        public void DeleteAccount_ShouldRemoveAccount()
        {
            var manager = new FinanceManager();
            var facade = new AccountFacade(manager);
            var account = facade.CreateAccount("Account To Delete", 100m);

            bool result = facade.DeleteAccount(account.Id);

            Assert.True(result); // Проверяем, что удаление прошло успешно.
            Assert.DoesNotContain(account, manager.BankAccounts); // Проверяем, что счёт удалён из списка.
        }
    }
}