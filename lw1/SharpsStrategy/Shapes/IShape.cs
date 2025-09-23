using System;
using System.Collections.Generic;
using SharpsStrategy.Behaviors;

namespace SharpsStrategy.Shapes
{
    public struct Point
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public abstract class IShape
    {
        protected IGetToString ToStringBehavior;
        protected ICountPerimetr CountPerimetrBehavior;
        protected ICountArea CountAreaBehavior;
        protected List<Point> Points;

        protected IShape (List<Point> shapePoints)
        {
            if(shapePoints == null || shapePoints.Count == 0)
            {
                throw new ArgumentNullException("невозмжно создать такую фигуру");
            }
            Points = new List<Point>(shapePoints);
        }

        public string performToString()
        {
            return ToStringBehavior.GetToString(Points);
        }

        public float performPerimetr()
        {
            return CountPerimetrBehavior.CountPerimetr(Points);
        }
        
        public float performArea()
        {
            return CountAreaBehavior.CountArea(Points);
        }

    }
}
