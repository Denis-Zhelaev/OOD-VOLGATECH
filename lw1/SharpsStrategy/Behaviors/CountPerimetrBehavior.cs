using SharpsStrategy.Shapes;
using System;
using System.Collections.Generic;

namespace SharpsStrategy.Behaviors
{
    public class TriangleCountPerimetr : ICountPerimetr
    {
        public float CountPerimetr(List<Point> PointsToCount)
        {
            //a - 0 b - 1 c - 2, находим стороны
            float ab = (float)Math.Sqrt(
                Math.Pow(PointsToCount[0].X - PointsToCount[1].X, 2) +
                Math.Pow(PointsToCount[0].Y - PointsToCount[1].Y, 2));
            float bc = (float)Math.Sqrt(
                Math.Pow(PointsToCount[1].X - PointsToCount[2].X, 2) +
                Math.Pow(PointsToCount[1].Y - PointsToCount[2].Y, 2));
            float ca = (float)Math.Sqrt(
                            Math.Pow(PointsToCount[2].X - PointsToCount[0].X, 2) +
                            Math.Pow(PointsToCount[2].Y - PointsToCount[0].Y, 2));

            return (ab + bc + ca);
        }
    }

    public class RectangleCountPerimetr : ICountPerimetr
    {
        public float CountPerimetr(List<Point> PointsToCount)
        {
            float high = PointsToCount[1].Y - PointsToCount[0].Y;
            float width = PointsToCount[1].X - PointsToCount[0].X;

            return (2 * (high + width));
        }
    }

}
