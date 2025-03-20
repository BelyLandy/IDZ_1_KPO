// Определение класса MenuManager, который управляет меню в приложении.
using ClassLibrary;

public class MenuManager
{
    // Поле для хранения корневого меню.
    private Menu Root;

    /// <summary>
    /// Конструктор класса MenuManager, принимающий корневое меню.
    /// </summary>
    /// <param name="root">Корневое меню, с которого начнется выполнение.</param>
    public MenuManager(Menu root)
    {
        Root = root; // Инициализация корневого меню.
    }

    /// <summary>
    /// Запускает выполнение меню, начиная с корневого элемента.
    /// </summary>
    public void Run()
    {
        Root.Run(); // Вызов метода Run корневого меню для запуска выполнения.
    }
}
