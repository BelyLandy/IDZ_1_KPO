using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Класс, представляющий меню с возможностью выполнения различных действий.
    /// После выполнения любого действия, даже если возникла ошибка, управление возвращается в меню.
    /// </summary>
    public class Menu
    {
        // Заголовок меню.
        private string Header;

        // Список элементов меню.
        public List<MenuItem> Items { get; set; }

        // Корневое меню, к которому можно вернуться.
        private Menu Root;

        // Вспомогательные методы для красивого вывода.
        private HelpMethods _helpMethods;

        /// <summary>
        /// Инициализирует новый экземпляр класса Menu с заданными параметрами.
        /// </summary>
        /// <param name="header">Заголовок меню.</param>
        /// <param name="items">Список элементов меню.</param>
        /// <param name="root">Корневое меню для возврата.</param>
        public Menu(string header, List<MenuItem> items, Menu root)
        {
            Header = header;
            Items = items;
            Root = root;
            _helpMethods = new HelpMethods();
        }

        /// <summary>
        /// Выводит на экран заголовок и элементы меню.
        /// </summary>
        private void Print()
        {
            _helpMethods.NiceOutput("Финансовый менеджер", title: 2, color: ConsoleColor.Magenta, clear: true);
            _helpMethods.NiceOutput(Header, title: 2, color: ConsoleColor.Cyan);

            for (int index = 0; index < Items.Count; index++)
            {
                _helpMethods.NiceOutput($"{index + 1}) ", ConsoleColor.Green, title: 0);
                _helpMethods.NiceOutput(Items[index].Title, color: ConsoleColor.Yellow);
            }

            _helpMethods.NiceOutput($"{Items.Count + 1}) ", color: ConsoleColor.Green, title: 0);
            _helpMethods.NiceOutput((Root == null ? "Завершить приложение." : "Вернуться в предыдущее меню."), color: ConsoleColor.Yellow);
        }

        /// <summary>
        /// Запускает бесконечный цикл, в котором отображается меню и выполняется выбранное действие.
        /// Если в процессе выполнения действия возникает ошибка, она обрабатывается и после этого выводится меню заново.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Print();
                uint item = GetUserItem();

                if (item == Items.Count + 1)
                {
                    if (Root == null)
                    {
                        Console.Clear();
                        _helpMethods.NiceOutput("Приложение завершено! Работу выполнил Девятов Денис Сергеевич БПИ-238", ConsoleColor.Cyan, title: 1);
                        Environment.Exit(0);
                    }
                    else
                    {
                        // Возврат в родительское меню.
                        return;
                    }
                }
                else
                {
                    try
                    {
                        // Выполнение действия выбранного пункта меню.
                        Items[(int)item - 1].Action?.Invoke();
                    }
                    catch (Exception ex)
                    {
                        _helpMethods.NiceOutput("Ошибка: " + ex.Message, ConsoleColor.Red);
                    }
                    // После выполнения или ошибки - возвращаемся к меню.
                    PromptToContinue();
                }
            }
        }

        /// <summary>
        /// Получает номер выбранного пользователем пункта меню.
        /// </summary>
        /// <returns>Номер выбранного пункта меню.</returns>
        private uint GetUserItem()
        {
            uint item = 0;
            Action getInput = () =>
            {
                Console.WriteLine();
                _helpMethods.NiceOutput("Выберите пункт меню: ", title: 0, color: ConsoleColor.Cyan);
                Console.ForegroundColor = ConsoleColor.Green;
                uint.TryParse(Console.ReadLine(), out item);
                Console.ResetColor();
            };
            getInput();
            while (item < 1 || item > Items.Count + 1)
            {
                Console.WriteLine();
                _helpMethods.NiceOutput("Ошибка ввода! Попробуйте еще раз!", color: ConsoleColor.Red);
                getInput();
            }
            return item;
        }

        /// <summary>
        /// После выполнения действия выводит приглашение для продолжения.
        /// </summary>
        private void PromptToContinue()
        {
            _helpMethods.NiceOutput("\nНажмите любую клавишу для возврата в меню...", ConsoleColor.Yellow);
            Console.ReadKey();
            Console.Clear();
        }
    }
}
