
namespace ClassLibrary
{
    /// <summary>
    /// Класс Category представляет категорию, которая имеет уникальный идентификатор,
    /// тип категории и название.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Уникальный идентификатор категории.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Тип категории, определяемый перечислением CategoryType.
        /// </summary>
        public CategoryType Type { get; set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Конструктор без параметров, необходим для десериализации.
        /// </summary>
        public Category() { }

        /// <summary>
        /// Создает новый экземпляр категории с указанным типом и названием.
        /// </summary>
        /// <param name="type">Тип категории.</param>
        /// <param name="name">Название категории.</param>
        public Category(CategoryType type, string name)
        {
            Id = Guid.NewGuid();
            Type = type;
            Name = name;
        }

        /// <summary>
        /// Возвращает строковое представление категории в формате: [Id] Name (Type).
        /// </summary>
        /// <returns>Строковое представление категории.</returns>
        public override string ToString() => $"[{Id}] {Name} ({Type})";
    }
}
