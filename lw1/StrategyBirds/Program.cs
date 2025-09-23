using System;
using System.Collections.Generic;
using StrategyBirds.Behaviors;
using StrategyBirds.Birds;

class Program
{
    static List<IBird> birds = new List<IBird>();

    static void Main(string[] args)
    {

        while (true)
        {
            Console.WriteLine("\nВсего птичек: " + birds.Count);
            Console.WriteLine("1 - Добавить птичку");
            Console.WriteLine("2 - Заставить всех птиц крякнуть");
            Console.WriteLine("3 - Заставить всех птиц полететь");
            Console.WriteLine("4 - Заставить всех птиц поплыть");
            Console.WriteLine("0 - Выход");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateBird();
                    break;
                case "2":
                    MakeAllBirdsMakeSound();
                    break;
                case "3":
                    MakeAllBirdsFly();
                    break;
                case "4":
                    MakeAllBirdsSwim();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }
    }

    static void CreateBird()
    {
        Console.WriteLine("\nНазовите свою птичку ");
        string birdName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(birdName)) { birdName = "Неизввестная птичка" + birds.Count + 1; }; //немного нумерации чтобы было легче ориентироваться
        
        Console.WriteLine("\nКакой вид у вашей птички?");
        Console.WriteLine("1 - Создать утку");
        Console.WriteLine("2 - Создать пингвина");
        Console.WriteLine("3 - Создать реактивную уточку");
        Console.WriteLine("0 - Выход");
        Console.Write("Выберите действие: ");

        var choice = Console.ReadLine();
        Console.Clear();
        switch (choice)
        {
            case "1":
                birds.Add(new Duck(birdName));
                break;
            case "2":
                birds.Add(new Pinguin(birdName));
                break;
            case "3":
                birds.Add(new ReactiveDuck(birdName));
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Неверный выбор!");
                break;
        }
    }

    static void MakeAllBirdsMakeSound()
    {
        if (birds.Count == 0)
        {
            Console.WriteLine("Нет птиц!");
            return;
        }

        foreach (var bird in birds)
        {
            bird.PerformSound();
        }
    }

    static void MakeAllBirdsFly()
    {
        if (birds.Count == 0)
        {
            Console.WriteLine("Нет птиц!");
            return;
        }

        foreach (var bird in birds)
        {
            bird.PerformFly();
        }
    }

    static void MakeAllBirdsSwim()
    {
        if (birds.Count == 0)
        {
            Console.WriteLine("Нет птиц!");
            return;
        }

        foreach (var bird in birds)
        {
            bird.PerformSwim();
        }
    }
}