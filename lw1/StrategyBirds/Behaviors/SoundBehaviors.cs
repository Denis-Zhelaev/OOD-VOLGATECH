using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyBirds.Behaviors
{
    public class SimpleQuack : ISoundBehavior
    {
        public void MakeSound()
        {
            Console.WriteLine("крякает: Кря-кря!");
        }
    }

    public class CanNotMakeSound : ISoundBehavior
    {
        public void MakeSound()
        {
            Console.WriteLine("не умеет издавать звуки :(");
        }
    }

    public class SimpleBeep : ISoundBehavior
    {
        public void MakeSound()
        {
            Console.WriteLine("пищит тонко");
        }
    }

}
