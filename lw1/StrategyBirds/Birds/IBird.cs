using StrategyBirds.Behaviors;
using System;

namespace StrategyBirds.Birds
{
    public abstract class IBird
    {
        protected IFlyBehavior flyBehavior;
        protected ISoundBehavior soundBehavior;
        protected ISwimBehavior swimBehavior;

        public string Name { get; set; }

        public void PerformFly()
        {
            Console.Write(Name + " ");
            flyBehavior.Fly();
        }

        public void PerformSound()
        {
            Console.Write(Name + " ");
            soundBehavior.MakeSound();
        }

        public void PerformSwim()
        {
            Console.Write(Name + " ");
            swimBehavior.Swim();
        }
    }

}
