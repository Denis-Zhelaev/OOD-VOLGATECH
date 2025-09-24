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
1. сделаю визуальную часть зависимой от внутреннего состояния
2. подпишу метеостанции на события из треда с консолью
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

        private const int MAX_STATIONS = 6;
        private const int COLUMNS = 3;
        private const int STATION_WIDTH = 400;
        private const int STATION_HEIGHT = 300;

        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(1200, 800), "Meteo Stations");

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

            stations[0] = new MeteoStation(0, 0);
            stationsCount = 1;

            consoleThread = new Thread(ConsoleWorker);
            consoleThread.Start();

            while (window.IsOpen && !closeRequested)
            {
                window.DispatchEvents();

                if (closeRequested)
                    break;

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
            while (isRunning)
            {
                try
                {
                    if (Console.KeyAvailable)
                    {
                        string input = Console.ReadLine();

                        lock (lockObject)
                        {
                            if (input == "add")
                            {
                                if (stationsCount < MAX_STATIONS)
                                {
                                    int row = stationsCount / COLUMNS;
                                    int col = stationsCount % COLUMNS;
                                    float x = col * STATION_WIDTH;
                                    float y = row * STATION_HEIGHT;

                                    stations[stationsCount] = new MeteoStation(x, y);
                                    stationsCount++;

                                    Console.WriteLine($"Добавлена станция. Всего станций: {stationsCount}/{MAX_STATIONS}");
                                }
                                else
                                {
                                    Console.WriteLine($"Достигнут лимит в {MAX_STATIONS} станций.");
                                }
                            }
                            else if (input == "remove")
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
                            else if (input == "exit" || input == "quit")
                            {
                                Console.WriteLine("Завершение работы...");
                                closeRequested = true;
                                isRunning = false;
                                break; 
                            }
                            else if (input == "clear")
                            {
                                Console.Clear();
                                Console.WriteLine("Консоль очищена. Доступные команды: add, remove, exit, clear");
                            }
                            else if (!string.IsNullOrEmpty(input))
                            {
                                Console.WriteLine("Неизвестная команда. Доступные команды: add, remove, exit, clear");
                            }
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
                    Console.WriteLine($"Ошибка в консольном потоке: {ex.Message}");
                    break;
                }
            }
        }
    }
}