
namespace ClassLibrary
{
    /// <summary>
    /// Класс BankAccount представляет банковский счет с уникальным идентификатором,
    /// именем владельца и балансом.
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Уникальный идентификатор счета.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя владельца счета.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Текущий баланс счета.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Конструктор без параметров, необходим для десериализации.
        /// </summary>
        public BankAccount() { }

        /// <summary>
        /// Создает новый экземпляр банковского счета с указанным именем владельца и начальным балансом.
        /// </summary>
        /// <param name="name">Имя владельца счета.</param>
        /// <param name="initialBalance">Начальный баланс счета. По умолчанию равен 0.</param>
        public BankAccount(string name, decimal initialBalance = 0)
        {
            Id = Guid.NewGuid();
            Name = name;
            Balance = initialBalance;
        }

        /// <summary>
        /// Возвращает строковое представление банковского счета в формате: [Id] Name (Баланс: Balance).
        /// </summary>
        /// <returns>Строковое представление счета.</returns>
        public override string ToString() => $"[{Id}] {Name} (Баланс: {Balance})";
    }
}
