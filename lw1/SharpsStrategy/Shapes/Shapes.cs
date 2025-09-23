using SharpsStrategy.Behaviors;
using System;
using System.Collections.Generic;

namespace SharpsStrategy.Shapes
{
    public class Triangle : IShape
    {
        public Triangle(List<Point> shapePoints) : base(shapePoints)
        {
            if (Points.Count != 3)
            {
                throw new ArgumentException("Треугольник должен иметь три точки");
            }
            ToStringBehavior = new TriangleToString();
            CountPerimetrBehavior = new TriangleCountPerimetr();
            CountAreaBehavior = new TriangleCountArea();
        }
    }

    public class Rectangle : IShape
    {
        public Rectangle(List<Point> shapePoints) : base(shapePoints)
        {
            if (Points.Count != 2)
            {
                throw new ArgumentException("Прямоугольник должен иметь две точки");
            }
            ToStringBehavior = new RectangleToString();
            CountPerimetrBehavior = new RectangleCountPerimetr();
            CountAreaBehavior = new RectangleCountArea();
        }
    }

}
