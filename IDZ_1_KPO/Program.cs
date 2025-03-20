using Microsoft.Extensions.DependencyInjection;
using ClassLibrary;

namespace KDZ_2
{
    public class Program
    {
        public static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            // Настройка DI-контейнера.
            var services = new ServiceCollection();

            // Регистрация зависимостей.
            services.AddSingleton<FinanceManager>();
            services.AddSingleton<AccountFacade>();
            services.AddSingleton<CategoryFacade>();
            services.AddSingleton<OperationFacade>();
            services.AddSingleton<FinanceOperations>();
            services.AddSingleton<HelpMethods>();
            services.AddTransient<DataExportImport>();

            // Собираем провайдер.
            var serviceProvider = services.BuildServiceProvider();

            try
            {
                // Разрешаем зависимости через DI-контейнер.
                var financeOperations = serviceProvider.GetRequiredService<FinanceOperations>();
                var financeManager = serviceProvider.GetRequiredService<FinanceManager>();
                var helpMethods = serviceProvider.GetRequiredService<HelpMethods>();

                // Создаем пункты главного меню.
                var itemsMainMenu = new List<MenuItem>();
                var headerMainMenu = "Главное меню управления финансами:";

                // Корневое меню. Если отсутствует родительское меню, выбор последнего пункта завершает приложение.
                var root = new Menu(headerMainMenu, itemsMainMenu, null);

                // Добавляем пункты меню с использованием методов из FinanceOperations.
                itemsMainMenu.Add(new MenuItem("Создать счет.", () => financeOperations.CreateAccount()));
                itemsMainMenu.Add(new MenuItem("Редактировать счет.", () => financeOperations.EditAccount()));
                itemsMainMenu.Add(new MenuItem("Удалить счет.", () => financeOperations.DeleteAccount()));
                itemsMainMenu.Add(new MenuItem("Создать категорию.", () => financeOperations.CreateCategory()));
                itemsMainMenu.Add(new MenuItem("Редактировать категорию.", () => financeOperations.EditCategory()));
                itemsMainMenu.Add(new MenuItem("Удалить категорию.", () => financeOperations.DeleteCategory()));
                itemsMainMenu.Add(new MenuItem("Создать операцию.", () => financeOperations.CreateOperation()));
                itemsMainMenu.Add(new MenuItem("Редактировать операцию.", () => financeOperations.EditOperation()));
                itemsMainMenu.Add(new MenuItem("Удалить операцию.", () => financeOperations.DeleteOperation()));
                itemsMainMenu.Add(new MenuItem("Показать данные.", () => financeOperations.ShowData()));
                itemsMainMenu.Add(new MenuItem("Подсчитать разницу доходов и расходов.", () => financeOperations.DisplayIncomeExpenseDifference()));
                itemsMainMenu.Add(new MenuItem("Группировать операции по категориям.", () => financeOperations.DisplayOperationsGroupedByCategory()));
                itemsMainMenu.Add(new MenuItem("Дополнительная аналитика.", () => financeOperations.DisplayAdditionalAnalytics()));
                itemsMainMenu.Add(new MenuItem("Экспорт данных.", () =>
                {
                    // Разрешаем DataExportImport через DI.
                    var dei = serviceProvider.GetRequiredService<DataExportImport>();
                    dei.ExportAllData();
                }));
                itemsMainMenu.Add(new MenuItem("Импорт данных.", () =>
                {
                    var dei = serviceProvider.GetRequiredService<DataExportImport>();
                    dei.ImportData();
                }));
                itemsMainMenu.Add(new MenuItem("Измерить время пересчёта балансов.", () => financeOperations.MeasureRecalculateBalancesTime()));

                // Запуск меню через MenuManager
                var menuManager = new MenuManager(root);
                menuManager.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            finally
            {
                Console.WriteLine();
            }
        }
    }
}
