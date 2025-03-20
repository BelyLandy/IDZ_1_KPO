using ClassLibrary;
using Xunit;

namespace UnitTestsProject
{
    /// <summary>
    /// Класс для тестирования функциональности фасада работы с категориями (CategoryFacade).
    /// </summary>
    public class CategoryFacadeTests
    {
        /// <summary>
        /// Тест для проверки создания категории.
        /// Убеждаемся, что категория добавляется в список категорий и её данные корректны.
        /// </summary>
        [Fact]
        public void CreateCategory_ShouldAddCategory()
        {
            var manager = new FinanceManager();
            var facade = new CategoryFacade(manager);
            string name = "Food";
            CategoryType type = CategoryType.Expense;

            var category = facade.CreateCategory(type, name);

            Assert.Contains(category, manager.Categories); // Проверяем, что категория добавлена в список.
            Assert.Equal(name, category.Name); // Проверяем корректность названия.
            Assert.Equal(type, category.Type); // Проверяем корректность типа.
        }

        /// <summary>
        /// Тест для проверки редактирования категории.
        /// Убеждаемся, что данные категории обновляются корректно.
        /// </summary>
        [Fact]
        public void EditCategory_ShouldUpdateCategory()
        {
            var manager = new FinanceManager();
            var facade = new CategoryFacade(manager);
            var category = facade.CreateCategory(CategoryType.Income, "Salary");
            string newName = "Bonus";
            CategoryType newType = CategoryType.Income;

            bool result = facade.EditCategory(category.Id, newName, newType);

            Assert.True(result); // Проверяем, что редактирование прошло успешно.
            Assert.Equal(newName, category.Name); // Проверяем обновлённое название.
            Assert.Equal(newType, category.Type); // Проверяем обновлённый тип.
        }

        /// <summary>
        /// Тест для проверки удаления категории.
        /// Убеждаемся, что категория удаляется из списка категорий.
        /// </summary>
        [Fact]
        public void DeleteCategory_ShouldRemoveCategory()
        {
            var manager = new FinanceManager();
            var facade = new CategoryFacade(manager);
            var category = facade.CreateCategory(CategoryType.Expense, "Groceries");

            bool result = facade.DeleteCategory(category.Id);

            Assert.True(result); // Проверяем, что удаление прошло успешно.
            Assert.DoesNotContain(category, manager.Categories); // Проверяем, что категория удалена из списка.
        }
    }
}