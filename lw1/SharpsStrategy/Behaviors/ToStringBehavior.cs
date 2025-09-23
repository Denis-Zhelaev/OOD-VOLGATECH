using SharpsStrategy.Shapes;
using System.Collections.Generic;


namespace SharpsStrategy.Behaviors
{
    public class TriangleToString : IGetToString
    {
        public string GetToString(List<Point> PointsToCount)
        {
            string output = "\nЭто треугольник" +
                            "\nТочка 1: X - " + PointsToCount[0].X + " Y - " + PointsToCount[0].Y+
                            "\nТочка 2: X - " + PointsToCount[1].X + " Y - " + PointsToCount[1].Y +
                            "\nТочка 3: X - " + PointsToCount[2].X + " Y - " + PointsToCount[2].Y;
            return output;
        }
    }

    public class RectangleToString : IGetToString
    {
        public string GetToString(List<Point> PointsToCount)
        {
            string output = "\nЭто прямоугольник" +
                           "\nТочка 1: X - " + PointsToCount[0].X + " Y - " + PointsToCount[0].Y +
                           "\nТочка 2: X - " + PointsToCount[1].X + " Y - " + PointsToCount[1].Y;
            return output;
        }
    }

}
