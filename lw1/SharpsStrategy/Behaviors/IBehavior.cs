using SharpsStrategy.Shapes;
using System;
using System.Collections.Generic;


namespace SharpsStrategy.Behaviors
{
    public interface ICountPerimetr
    {
        float CountPerimetr(List<Point> PointsToCount);
    }

    public interface ICountArea
    {
        float CountArea(List<Point> PointsToCount);

    }
}
