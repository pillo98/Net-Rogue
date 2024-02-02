using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    public class Program
    {
        public static void Main()
        {

            
            Game rogue = new Game();
            rogue.Run();
            Console.Clear();
            while (true)
            {
                PlayGame Game = new PlayGame();
                Game.Run();
            }

        }
    }
}
