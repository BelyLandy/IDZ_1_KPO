namespace ClassLibrary
{
    /// <summary>
    /// Представляет элемент меню, содержащий заголовок и действие, выполняемое при выборе пункта.
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Заголовок пункта меню.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Действие, ассоциированное с пунктом меню.
        /// </summary>
        public Action? Action { get; }

        /// <summary>
        /// Создает экземпляр <see cref="MenuItem"/> с указанным заголовком и действием.
        /// </summary>
        /// <param name="title">Заголовок пункта меню.</param>
        /// <param name="action">Действие, выполняемое при выборе пункта меню.</param>
        public MenuItem(string title, Action? action)
        {
            Title = title; // Установка заголовка пункта меню.
            Action = action; // Установка действия пункта меню.
        }
    }
}
