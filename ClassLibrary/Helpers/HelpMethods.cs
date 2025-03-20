namespace ClassLibrary
{
    /// <summary>
    /// Класс, содержащий вспомогательные методы для обработки пользовательского ввода и форматированного вывода в консоль.
    /// </summary>
    public class HelpMethods
    {
        /// <summary>
        /// Получает путь к файлу от пользователя с проверкой корректности пути и расширения.
        /// </summary>
        /// <param name="operationType">Тип операции: "read" для чтения или "save" для сохранения файла.</param>
        /// <returns>Корректный путь к файлу или null, если пользователь отказался от ввода.</returns>
        public string? GetFilePath(string operationType = "read")
        {
            string filePath;

            while (true)
            {
                // Ввод пути в зависимости от типа операции.
                if (operationType == "read")
                {
                    filePath = GetString("Введите путь к файлу для чтения (\"b\" - выйти в меню): ");
                }
                else if (operationType == "save")
                {
                    filePath = GetString("Введите путь для сохранения файла (\"b\" - выйти в меню): ");
                }
                else
                {
                    Console.WriteLine("Неверный тип операции.");
                    return null;
                }

                // Проверка на выход из метода.
                if (filePath == "b")
                {
                    return null;
                }

                // Удаляем кавычки, если они есть.
                if (filePath[0] == '"' && filePath[^1] == '"')
                {
                    filePath = filePath[1..^1];
                }

                // Проверка существования директории для сохранения.
                if (operationType == "save" && !Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Console.WriteLine("Указанный путь не существует. Попробуйте снова.");
                    continue;
                }

                // Проверка расширения и существования файла для чтения.
                if (operationType == "read")
                {
                    string extension = Path.GetExtension(filePath).ToLower();
                    if (extension != ".csv" && extension != ".json" && extension != ".yaml" && extension != ".yml")
                    {
                        Console.WriteLine();
                        NiceOutput("Расширение файла не соответствует спецификации! Попробуйте снова!", color: ConsoleColor.Red);
                        Console.WriteLine();
                        continue;
                    }
                    else if (!new FileInfo(filePath).Exists)
                    {
                        Console.WriteLine();
                        NiceOutput("По указанному пути файл не найден! Попробуйте снова!", color: ConsoleColor.Red);
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        break; // Путь корректен.
                    }
                }
                else if (operationType == "save")
                {
                    break; // Путь корректен для сохранения.
                }
            }

            return filePath; // Возвращаем корректный путь.
        }


        /// <summary>
        /// Выводит текст в консоль с возможностью настройки цвета, скорости печати, и задержки после вывода.
        /// </summary>
        /// <param name="output">Текст для вывода.</param>
        /// <param name="color">Цвет текста в консоли.</param>
        /// <param name="speed">Скорость печати текста в миллисекундах на символ.</param>
        /// <param name="title">Количество переводов строки после текста.</param>
        /// <param name="clear">Флаг для очистки консоли перед выводом.</param>
        /// <param name="sleep">Задержка в миллисекундах после вывода текста.</param>
        public void NiceOutput(string output, ConsoleColor color = ConsoleColor.White, byte speed = 0, ushort title = 1, bool clear = false, ushort sleep = 0)
        {
            if (clear)
            {
                Console.Clear(); // Очистка консоли.
            }

            Console.ForegroundColor = color; // Установка цвета текста.

            // Печать текста с задержкой на каждый символ.
            foreach (char symbol in output)
            {
                Console.Write(symbol);
                Thread.Sleep(speed);
            }

            Console.ForegroundColor = ConsoleColor.White; // Сброс цвета текста.

            // Добавление заданного количества переводов строки.
            if (title > 0)
            {
                for (int i = 0; i < title; ++i)
                {
                    Console.WriteLine();
                }
            }

            // Задержка после вывода текста.
            if (sleep > 0)
            {
                Thread.Sleep(sleep);
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Запрашивает у пользователя строку с возможностью настройки цвета текста запроса и ввода.
        /// </summary>
        /// <param name="header">Сообщение, отображаемое перед вводом.</param>
        /// <param name="_color">Цвет текста запроса.</param>
        /// <param name="readColor">Цвет текста, вводимого пользователем.</param>
        /// <returns>Введенная пользователем строка.</returns>
        public string GetString(string header = "Введите строку: ", ConsoleColor _color = ConsoleColor.Yellow, ConsoleColor readColor = ConsoleColor.Green)
        {
            string? str;

            while (true) // Цикл для запроса строки до тех пор, пока она не будет непустой.
            {
                NiceOutput(output: header, color: _color, title: 0); // Вывод запроса.

                Console.ForegroundColor = readColor; // Установка цвета текста ввода.
                str = Console.ReadLine();
                Console.ResetColor(); // Сброс цвета текста.

                if (!string.IsNullOrEmpty(str))
                {
                    break; // Выход из цикла, если строка не пустая.
                }

                Console.WriteLine();
                NiceOutput("Строка равна null, или пустая! Пожалуйста, введите не пустую строку!", color: ConsoleColor.Red);
                Console.WriteLine();
            }

            return str; // Возвращение введенной строки.
        }
    }
}
