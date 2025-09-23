using System;
using System.Collections.Generic;

public interface IFlyBehavior
{
    void Fly();
}

public interface IQuackBehavior
{
    void Quack();
}

public interface ISwimBehavior
{
    void Swim();
}

public class SimpleFly : IFlyBehavior
{
    public void Fly()
    {
        Console.WriteLine("летит в небе");
    }
}

public class CanNotFly : IFlyBehavior
{
    public void Fly()
    {
        Console.WriteLine("не умеет летать:(");
    }
}

public class SimpleQuack : IQuackBehavior
{
    public void Quack()
    {
        Console.WriteLine("крякает: Кря-кря!");
    }
}

public class CanNotQuack : IQuackBehavior
{
    public void Quack()
    {
        Console.WriteLine("не умеет крякать:(");
    }
}

public class SimpleSwim : ISwimBehavior
{
    public void Swim()
    {
        Console.WriteLine("плывет по воде");
    }
}

public class CanNotSwim : ISwimBehavior
{
    public void Swim()
    {
        Console.WriteLine("Птичка не умеет плавать:(");
    }
}

public abstract class Bird
{
    protected IFlyBehavior flyBehavior;
    protected IQuackBehavior quackBehavior;
    protected ISwimBehavior swimBehavior;

    public string Name { get; set; }

    public void PerformFly()
    {
        Console.Write(Name + " ");
        flyBehavior.Fly();
    }

    public void PerformQuack()
    {
        Console.Write(Name + " ");
        quackBehavior.Quack();
    }

    public void PerformSwim()
    {
        Console.Write(Name + " ");
        swimBehavior.Swim();
    }
}

public class Duck : Bird
{
    public Duck(string name)
    {
        Name = name;
        flyBehavior = new SimpleFly();
        quackBehavior = new SimpleQuack();
        swimBehavior = new SimpleSwim();
    }
}

public class Pinguin : Bird
{
    public Pinguin(string name)
    {
        Name = name;
        flyBehavior = new CanNotFly();
        quackBehavior = new SimpleQuack();
        swimBehavior = new SimpleSwim();
    }
}

class Program
{
    static List<Bird> birds = new List<Bird>();

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
                    MakeAllBirdsQuack();
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
        if (string.IsNullOrWhiteSpace(birdName)) { birdName = "Неизввестная птичка"; }
        ;

        Console.WriteLine("\nКакой вид у вашей птички?");
        Console.WriteLine("1 - Создать утку");
        Console.WriteLine("2 - Создать пингвина");
        Console.WriteLine("0 - Выход");
        Console.Write("Выберите действие: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                birds.Add(new Duck(birdName));
                break;
            case "2":
                birds.Add(new Pinguin(birdName));
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Неверный выбор!");
                break;
        }
    }

    static void MakeAllBirdsQuack()
    {
        if (birds.Count == 0)
        {
            Console.WriteLine("Нет птиц!");
            return;
        }

        foreach (var bird in birds)
        {
            bird.PerformQuack();
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