
namespace ClassLibrary
{
    /// <summary>
    /// Конкретная команда для создания счета.
    /// Инкапсулирует логику создания счета через фасад.
    /// </summary>
    public class CreateAccountCommand : ICommand
    {
        private readonly AccountFacade _accountFacade;
        private readonly string _name;
        private readonly decimal _balance;

        /// <summary>
        /// Результат выполнения команды — созданный банковский счет.
        /// </summary>
        public BankAccount Result { get; private set; }

        /// <summary>
        /// Создает экземпляр команды для создания счета.
        /// </summary>
        /// <param name="facade">Фасад для работы со счетами.</param>
        /// <param name="name">Имя владельца счета.</param>
        /// <param name="balance">Начальный баланс счета.</param>
        public CreateAccountCommand(AccountFacade facade, string name, decimal balance)
        {
            _accountFacade = facade;
            _name = name;
            _balance = balance;
        }

        /// <summary>
        /// Выполняет команду создания счета и сохраняет результат в свойстве Result.
        /// </summary>
        public void Execute()
        {
            Result = _accountFacade.CreateAccount(_name, _balance);
        }
    }
}
