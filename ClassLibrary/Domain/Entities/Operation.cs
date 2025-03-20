
namespace ClassLibrary
{
    /// <summary>
    /// Класс Operation представляет финансовую операцию, которая имеет уникальный идентификатор,
    /// тип операции, идентификатор банковского счета, сумму, дату, описание и идентификатор категории.
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Уникальный идентификатор операции.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Тип операции, определяемый перечислением OperationType.
        /// </summary>
        public OperationType Type { get; set; }

        /// <summary>
        /// Идентификатор банковского счета, связанного с операцией.
        /// </summary>
        public Guid BankAccountId { get; set; }

        /// <summary>
        /// Сумма операции.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Дата выполнения операции.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Описание операции.
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Идентификатор категории, связанной с операцией.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Конструктор без параметров, необходим для десериализации.
        /// </summary>
        public Operation() { }

        /// <summary>
        /// Создает новый экземпляр операции с указанными параметрами.
        /// </summary>
        /// <param name="type">Тип операции.</param>
        /// <param name="bankAccountId">Идентификатор банковского счета.</param>
        /// <param name="amount">Сумма операции.</param>
        /// <param name="date">Дата выполнения операции.</param>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="description">Описание операции. По умолчанию пустая строка.</param>
        public Operation(OperationType type, Guid bankAccountId, decimal amount, DateTime date, Guid categoryId, string description = "")
        {
            Id = Guid.NewGuid();
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Description = description;
        }

        /// <summary>
        /// Возвращает строковое представление операции в формате: [Id] Type на сумму Amount от Date (Счет: BankAccountId, Категория: CategoryId).
        /// </summary>
        /// <returns>Строковое представление операции.</returns>
        public override string ToString() => $"[{Id}] {Type} на сумму {Amount} от {Date.ToShortDateString()} (Счет: {BankAccountId}, Категория: {CategoryId})";
    }
}
