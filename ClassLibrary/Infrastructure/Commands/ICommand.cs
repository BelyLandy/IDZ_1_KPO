
namespace ClassLibrary
{
    /// <summary>
    /// Интерфейс команды.
    /// Каждый пользовательский сценарий инкапсулируется в отдельную команду.
    /// </summary>
    public interface ICommand
    {
        void Execute();
    }
}
