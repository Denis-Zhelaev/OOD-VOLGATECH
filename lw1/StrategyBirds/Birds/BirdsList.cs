using StrategyBirds.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyBirds.Birds
{
    public class Duck : IBird
    {
        public Duck(string name)
        {
            Name = name;
            flyBehavior = new SimpleFly();
            soundBehavior = new SimpleQuack();
            swimBehavior = new SimpleSwim();
        }
    }

    public class Pinguin : IBird
    {
        public Pinguin(string name)
        {
            Name = name;
            flyBehavior = new CanNotFly();
            soundBehavior = new SimpleBeep(); //пингвины пищат
            swimBehavior = new SwimmingAndDiving();
        }
    }

    public class ReactiveDuck : IBird //реактивная уточка искусственная
    {
        public ReactiveDuck(string name)
        {
            Name = name;
            flyBehavior = new ReactiveFly(); //реактивно летает
            soundBehavior = new CanNotMakeSound();//не издаёт звуков и не плавает
            swimBehavior = new CanNotSwim();
        }
    }

}
