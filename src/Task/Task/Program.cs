using Task;

class Program
{
    static void Main(string[] args)
    {
        Sorter sorter = new Sorter();
        sorter.InitCollection();

        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n=== МЕНЮ КОЛЕКЦІЇ ПУБЛІКАЦІЙ ===");
            Console.WriteLine("1. Створити порожню колекцію (Очистити)");
            Console.WriteLine("2. Додати запис (з клавіатури)");
            Console.WriteLine("3. Видалити запис за ID");
            Console.WriteLine("4. Вивести поточний вміст колекції");
            Console.WriteLine("5. Згенерувати контрольні дані (12 записів)");
            Console.WriteLine("6. Відсортувати колекцію (Merge Sort)");
            Console.WriteLine("7. Вивести статистику сортування");
            Console.WriteLine("8. Вивести Топ-10 публікацій (Варіант 11)");
            Console.WriteLine("0. Вийти з програми");
            Console.Write("Оберіть дію: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    sorter.InitCollection();
                    Console.WriteLine("Колекцію успішно очищено та ініціалізовано.");
                    break;
                case "2":
                    int id;
                    while(true)
                    {
                        Console.Write("Введіть ID публікації (>=0):");
                        if (int.TryParse(Console.ReadLine(), out id) && id >= 0)
                        {
                            break;
                        }
                        Console.WriteLine("Значення невалідне, спробуйте ще раз!");
                    }

                    Console.Write("Введіть прізвище автора: ");
                    string author = Console.ReadLine();

                    Console.Write("Введіть назву публікації: ");
                    string title = Console.ReadLine();

                    int citations;

                    while (true)
                    {
                        Console.Write("Введіть к-сть цитувань (>=0):");
                        if (int.TryParse(Console.ReadLine(), out citations) && citations >= 0)
                        {
                            break;
                        }
                        Console.WriteLine("Значення невалідне, спробуйте ще раз!");
                    }

                    sorter.AddRecord(new Record(id, author, title, citations));
                    break;

                case "3":
                    Console.Write("Введіть ID для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int removeId))
                    {
                        sorter.RemoveRecord(removeId);
                    }
                    else
                    {
                        Console.WriteLine("Помилка: число не отримано.");
                    }
                    break;

                case "4":
                    sorter.PrintCollection();
                    break;

                case "5":
                    sorter.GenerateControlData();
                    break;

                case "6":
                    Console.WriteLine("Старт сортування...");
                    sorter.SortCollection();
                    break;

                case "7":
                    sorter.PrintStatistics();
                    break;

                case "8":
                    sorter.PrintTop10();
                    break;

                case "0":
                    isRunning = false;
                    Console.WriteLine("Дякую за використання!");
                    break;

                default:
                    Console.WriteLine("Невідома команда. Будь ласка, оберіть цифру з меню (0-8).");
                    break;
            }
        }
    }
}