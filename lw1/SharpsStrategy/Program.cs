using SFML.Graphics;
using SharpsStrategy.Shapes;
using System;
using System.Collections.Generic;
using System.Reflection;

/*
Напишите программу, работающую с фигурами
Программа должна уметь рассчитывать площадь и периметр,
а так же выводить данные о фигурах.
Для реализации функционала используйте паттерн "стратегия".

Был написан на С# в целях ознакомления с синтаксисом. 
 */

class Program
{
    static List<IShape> shapes = new List<IShape>();

    static void Main()
    {
        //добавил пару фигур для демонстрации
        shapes.Add(new Rectangle(new List<Point> { new Point(0, 0), new Point(1, 1) }));
        shapes.Add(new Triangle(new List<Point> { new Point(0, 0), new Point(1, 0), new Point(0.5f, 1) }));

        int indexOfShape = 0;

        while (true)
        {
            Console.WriteLine("Сколько фигур сейчас существует: " + shapes.Count);
            Console.WriteLine("1. - вывести список всех фигур");
            Console.WriteLine("2. - рассчитать периметр определённой фигуры");
            Console.WriteLine("3. - рассчитать площадь определённой фигуры");
            Console.WriteLine("4. - удалить определённую фигуру");
            Console.WriteLine("5. - добавить новую фигуру");
            Console.WriteLine("0. - выйти из приложения");

            var choice = Console.ReadLine();
            switch (choice) 
            {
                case "1":
                    for(int i = 0; i < shapes.Count; i++)
                    {
                        Console.WriteLine(shapes[i].performToString());
                    }    
                    break;
                case "2":
                    Console.WriteLine("\n Выберите индекс нужной вам фигуры (начиная с 0)");
                    if (!int.TryParse(Console.ReadLine(), out indexOfShape))
                    {
                        Console.WriteLine("\nОшибка: Введено не число");
                        break;
                    }
                    //можно просто инкримент добавить но да ладно
                    if (indexOfShape < 0 || indexOfShape >= shapes.Count)
                    {
                        Console.WriteLine("\nОшибка, такой фигуры нет");
                        break;
                    }
                    Console.WriteLine("Периметр данной фигуры: " + shapes[indexOfShape].performPerimetr());
                    break;
                case "3":
                    Console.WriteLine("\n Выберите индекс нужной вам фигуры (начиная с 0)");

                    if (!int.TryParse(Console.ReadLine(), out indexOfShape))
                    {
                        Console.WriteLine("\nОшибка: Введено не число");
                        break;
                    }
                    if (indexOfShape < 0 || indexOfShape >= shapes.Count)
                    {
                        Console.WriteLine("\nОшибка, такой фигуры нет");
                        break;
                    }
                    Console.WriteLine("Площадь данной фигуры: " + shapes[indexOfShape].performArea());

                    break;
                case "4":
                    Console.WriteLine("\n Выберите индекс нужной вам фигуры");
                    if (!int.TryParse(Console.ReadLine(), out indexOfShape))
                    {
                        Console.WriteLine("\nОшибка: Введено не число");
                        break;
                    }
                    if (indexOfShape < 0 || indexOfShape >= shapes.Count)
                    {
                        Console.WriteLine("\nОшибка, такой фигуры нет");
                        break;
                    }
                    shapes.RemoveAt(indexOfShape);
                    break;
                case "5":
                    Console.WriteLine("Выберите тип фигуры: \n1. - треугольник \n2. Прямоугольник");
                    if (!int.TryParse(Console.ReadLine(), out int typeOfShape))
                    {
                        Console.WriteLine("\nОшибка: Введено не число");
                        break;
                    }
                    if (typeOfShape <= 0 || typeOfShape > 3)
                    {
                        Console.WriteLine("\nОшибка, такого типа фигуры нет");
                        break;
                    }

                    List<Point> points = new List<Point>();

                    switch (typeOfShape)
                    {
                        case 1:
                            Console.WriteLine("Введите координаты 3 точек треугольника через пробел (x y):");
                            while(true)
                            {
                                var coords = Console.ReadLine().Split();
                                if (coords.Length != 2 || !float.TryParse(coords[0], out float x) || !float.TryParse(coords[1], out float y))
                                {
                                    Console.WriteLine("Ошибка ввода координат");
                                }
                                else
                                {
                                    points.Add(new Point(x, y));

                                    if (points.Count == 3)
                                    {
                                        shapes.Add(new Triangle(points));
                                        Console.WriteLine("Треугольник добавлен!");
                                        break;
                                    }
                                }
                            }
                            break;
                        case 2:
                            Console.WriteLine("Введите координаты 2 точек(левый нижний и правый верхний) прямоугольника через пробел ( х у ):");
                            while (true)
                            {
                                var coords = Console.ReadLine().Split();
                                if (coords.Length != 2 || !float.TryParse(coords[0], out float x) || !float.TryParse(coords[1], out float y))
                                {
                                    Console.WriteLine("Ошибка ввода координат");
                                }
                                else
                                {
                                    points.Add(new Point(x, y));

                                    if (points.Count == 2)
                                    {
                                        shapes.Add(new Triangle(points));
                                        Console.WriteLine("Прямоугольник добавлен!");
                                        break;
                                    }
                                }
                            }
                            break;
                    }
                    break;
                case "0":
                    return;
                case "-1":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте ещё раз!");
                    Console.Clear();
                    break;
            }
        }
    }
}
