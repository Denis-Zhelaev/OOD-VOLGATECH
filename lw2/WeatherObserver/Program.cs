using System;
using System.Threading;
using SFML.Graphics;
using SFML.Window;


/*
Создать программу имитирующая сеть метеостанций, 
которая будет представлять собой несколько классов метеостанций.
Каждая метеостанция должна подписаться на уведомления об измене погоды 
и отображать это визуально.
Добавьте возможность выбирать на какие уведомления будет подписываться метеостанция при создании.
Добавьте возможность для метеостанции отписаться от определенных уведомлений.

Сделал каркас программы, далее
1. сделаю визуальную часть зависимой от внутреннего состояния - сделал
2. подпишу метеостанции на события из треда с консолью - создал доп класс погоды, который будет хранить состояние погоды и уведомлять станции
3. задам возможность выбирать подписки для метеостанции
4. доработаю класс метеостанции чтобы при отсутствии подписки индикатор визуально отображал, 
что не следит за изменениями.
 */
namespace MeteoStation
{
    class Program
    {
        private static RenderWindow window;
        private static MeteoStation[] stations = new MeteoStation[6];
        private static int stationsCount = 0;
        private static Thread consoleThread;
        private static bool isRunning = true;
        private static object lockObject = new object();
        private static bool closeRequested = false;

        private static Weather weather = new Weather();

        private const int MAX_STATIONS = 6;
        private const int COLUMNS = 3;
        private const int STATION_WIDTH = 400;
        private const int STATION_HEIGHT = 300;

        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(1200, 600), "Meteo Stations");

            window.Closed += (s, e) =>
            {
                isRunning = false;
                closeRequested = true;
                window.Close();
            };

            window.KeyPressed += (s, e) =>
            {
                if (e.Code == Keyboard.Key.Escape)
                {
                    isRunning = false;
                    closeRequested = true;
                    window.Close();
                }
            };

            stations[0] = new MeteoStation(0, 0, weather);
            stationsCount = 1;

            consoleThread = new Thread(ConsoleWorker);
            consoleThread.Start();

            while (window.IsOpen && !closeRequested)
            {
                window.DispatchEvents();

                if (closeRequested) { break; }

                window.Clear(Color.White);

                lock (lockObject)
                {
                    for (int i = 0; i < stationsCount; i++)
                    {
                        stations[i].Draw(window);
                    }
                }

                window.Display();
                Thread.Sleep(10);
            }

            isRunning = false;
            if (!consoleThread.Join(1000))
            {
                consoleThread.Interrupt();
            }

            if (window.IsOpen)
            {
                window.Close();
            }
        }

        private static void ConsoleWorker()
        {
            PrintHelp();
            Console.WriteLine();
            while (isRunning)
            {
                try
                {
                    if (Console.KeyAvailable)
                    {
                        string input = Console.ReadLine();

                        lock (lockObject)
                        {
                            ProcessCommand(input);
                        }
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
                catch (ThreadInterruptedException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("  help - показать команды");
            Console.WriteLine("  add - добавить метеостанцию");
            Console.WriteLine("  remove - удалить метеостанцию");
            Console.WriteLine("  weather.temp [значение] - установить температуру (-100 до 100)");
            Console.WriteLine("  weather.press [значение] - установить давление (0 до 2000)");
            Console.WriteLine("  weather.wind.velos [значение] - установить скорость ветра (0 до 100)");
            Console.WriteLine("  weather.wind.dir [значение] - установить направление ветра (0-360)");
            Console.WriteLine("  weather.check - показать текущее состояние погоды");
            Console.WriteLine("  clear - очистить консоль");
            Console.WriteLine("  exit - выход из программы");
        }

        private static void ProcessCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return;

            string[] parts = input.Split(' ');
            string command = parts[0].ToLower();

            switch (command)
            {
                case "help":
                    PrintHelp();
                    break;

                case "add":
                    AddStation();
                    break;

                case "remove":
                    RemoveStation();
                    break;

                case "weather.temp":
                    if (parts.Length > 1 && float.TryParse(parts[1], out float temp))
                    {
                        try
                        {
                            weather.Temperature = temp;
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Использование: weather.temp [значение]");
                    }
                    break;

                case "weather.press":
                    if (parts.Length > 1 && float.TryParse(parts[1], out float press))
                    {
                        try
                        {
                            weather.Pressure = press;
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Использование: weather.press [значение]");
                    }
                    break;

                case "weather.wind.velos":
                    if (parts.Length > 1 && float.TryParse(parts[1], out float speed))
                    {
                        try
                        {
                            weather.WindSpeed = speed;
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Использование: weather.wind.velos [значение]");
                    }
                    break;

                case "weather.wind.dir":
                    if (parts.Length > 1 && float.TryParse(parts[1], out float dir))
                    {
                        weather.WindDirection = dir;
                    }
                    else
                    {
                        Console.WriteLine("Использование: weather.wind.dir [значение]");
                    }
                    break;

                case "weather.check":
                    Console.WriteLine("Текущее состояние погоды:");
                    Console.WriteLine(weather.ToString());
                    break;

                case "exit":
                    Console.WriteLine("Завершение работы...");
                    closeRequested = true;
                    isRunning = false;
                    break;

                case "clear":
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Неизвестная команда. Введите 'help' для списка команд.");
                    break;
            }
        }
        private static void AddStation()
        {
            if (stationsCount < MAX_STATIONS)
            {
                int row = stationsCount / COLUMNS;
                int col = stationsCount % COLUMNS;
                float x = col * STATION_WIDTH;
                float y = row * STATION_HEIGHT;

                stations[stationsCount] = new MeteoStation(x, y, weather);
                stationsCount++;

                Console.WriteLine($"Добавлена станция. Всего станций: {stationsCount}/{MAX_STATIONS}");
            }
            else
            {
                Console.WriteLine($"Достигнут лимит в {MAX_STATIONS} станций.");
            }
        }

        private static void RemoveStation()
        {
            if (stationsCount > 0)
            {
                stationsCount--;
                stations[stationsCount] = null;
                Console.WriteLine($"Удалена станция. Всего станций: {stationsCount}/{MAX_STATIONS}");
            }
            else
            {
                Console.WriteLine("Нет станций для удаления.");
            }
        }
    }
}