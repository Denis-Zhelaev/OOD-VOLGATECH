using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyBirds.Behaviors
{
    public class SimpleSwim : ISwimBehavior
    {
        public void Swim()
        {
            Console.WriteLine("плывет по воде");
        }
    }

    public class CanNotSwim : ISwimBehavior
    {
        public void Swim()
        {
            Console.WriteLine("не умеет плавать:(");
        }
    }

    public class SwimmingAndDiving : ISwimBehavior
    {
        public void Swim()
        {
            Console.WriteLine("хорошо плавает и ныряет");
        }
    }

}
