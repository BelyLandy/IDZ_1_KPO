using System.Diagnostics;

namespace ClassLibrary
{
    /// <summary>
    /// Декоратор команды для измерения времени выполнения.
    /// Позволяет повторно использовать логику измерения для всех команд.
    /// </summary>
    public class CommandTimeDecorator : ICommand
    {
        private readonly ICommand _innerCommand;

        /// <summary>
        /// Время выполнения команды.
        /// </summary>
        public TimeSpan ExecutionTime { get; private set; }

        /// <summary>
        /// Создает экземпляр декоратора для измерения времени выполнения команды.
        /// </summary>
        /// <param name="command">Команда, время выполнения которой нужно измерить.</param>
        public CommandTimeDecorator(ICommand command)
        {
            _innerCommand = command;
        }

        /// <summary>
        /// Выполняет команду и измеряет время её выполнения.
        /// </summary>
        public void Execute()
        {
            var sw = Stopwatch.StartNew();
            _innerCommand.Execute();
            sw.Stop();
            ExecutionTime = sw.Elapsed;
            Console.WriteLine($"Время выполнения команды: {ExecutionTime.TotalMilliseconds} ms");
        }
    }
}
