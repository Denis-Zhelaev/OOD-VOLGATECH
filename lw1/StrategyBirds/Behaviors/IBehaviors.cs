using System;

namespace StrategyBirds.Behaviors
{
    public interface IFlyBehavior
    {
        void Fly();
    }

    public interface ISoundBehavior
    {
        void MakeSound();
    }

    public interface ISwimBehavior
    {
        void Swim();
    }
}
