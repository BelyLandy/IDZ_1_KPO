
using ClassLibrary.Domain;

namespace ClassLibrary
{
    /// <summary>
    /// Фасад для работы с категориями.
    /// </summary>
    public class CategoryFacade
    {
        private readonly FinanceManager _manager;

        /// <summary>
        /// Создает экземпляр фасада для работы с категориями.
        /// </summary>
        /// <param name="manager">Экземпляр FinanceManager, используемый для управления категориями.</param>
        public CategoryFacade(FinanceManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Создает новую категорию с указанным типом и именем.
        /// </summary>
        /// <param name="type">Тип категории (Income или Expense).</param>
        /// <param name="name">Название категории.</param>
        /// <returns>Созданная категория.</returns>
        public Category CreateCategory(CategoryType type, string name)
        {
            var category = DomainFactory.CreateCategory(type, name);
            _manager.Categories.Add(category);
            return category;
        }

        /// <summary>
        /// Редактирует существующую категорию, изменяя ее имя и/или тип.
        /// </summary>
        /// <param name="id">Идентификатор категории, которую нужно отредактировать.</param>
        /// <param name="newName">Новое название категории.</param>
        /// <param name="newType">Новый тип категории. Если не указан, тип остается неизменным.</param>
        /// <returns>True, если категория была успешно отредактирована, иначе False.</returns>
        public bool EditCategory(Guid id, string newName, CategoryType? newType = null)
        {
            var category = _manager.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return false;
            category.Name = newName;
            if (newType.HasValue)
                category.Type = newType.Value;
            return true;
        }

        /// <summary>
        /// Удаляет категорию по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории, которую нужно удалить.</param>
        /// <returns>True, если категория была успешно удалена, иначе False.</returns>
        public bool DeleteCategory(Guid id)
        {
            var category = _manager.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return false;
            _manager.Categories.Remove(category);
            return true;
        }
    }
}
