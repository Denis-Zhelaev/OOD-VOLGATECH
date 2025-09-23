using System;

namespace StrategyBirds.Behaviors
{
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

    public class ReactiveFly : IFlyBehavior
    {
        public void Fly()
        { 
            Console.WriteLine("летит на реактивной тяге"); 
        }
    }

}
