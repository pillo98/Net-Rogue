using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rogue
{
    internal class Game
    {
        public void Run()
        {
            PlayerCharacterS player = new PlayerCharacterS();
            while (true)
            {
                Console.WriteLine("What is your name?");
                string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Not acceptable");
                    continue;
                }
                bool nameOK = true;
                for (int i = 0; i < name.Length; i++)
                {
                    char kirjain = name[i];
                    if (Char.IsLetter(kirjain) == false)
                    {
                        nameOK = false;
                        player.name = name;
                        break;

                    }
                }
                if (nameOK == false) 
                {
                    Console.WriteLine("Name can only contain letters!");
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.WriteLine("Select race");
                Console.WriteLine(Race.Human.ToString());
                Console.WriteLine(Race.Elf.ToString());
                Console.WriteLine(Race.Orc.ToString());
                string race = Console.ReadLine();
                if (race == Race.Human.ToString())
                {
                    player.race = Race.Human;
                    break;
                }
                else if (race == Race.Elf.ToString())
                {
                    player.race = Race.Elf;
                    break;
                }
                else if (race == Race.Orc.ToString())
                {
                    player.race = Race.Orc;
                    break;
                }
                else
                {
                    Console.WriteLine("That is not a Race");
                    continue;
                }
            }
            while (true)
            {


                Console.WriteLine("Select Class");
                Console.WriteLine(Class.Rogue.ToString());
                Console.WriteLine(Class.Warrior.ToString());
                Console.WriteLine(Class.Mage.ToString());
                string Role = Console.ReadLine();
                if (Role == Class.Rogue.ToString())
                {
                    player.Role = Class.Rogue;
                    break;
                }
                else if (Role == Class.Warrior.ToString())
                {
                    player.Role = Class.Warrior;
                    break;
                }
                else if (Role == Class.Mage.ToString())
                {
                    player.Role = Class.Mage;
                    break;
                }
                else
                {
                    Console.WriteLine("That is not a Class");
                    continue;
                }
            }

        }
    }
    class PlayGame
    {
        public Map Level01;
        public void Run()
        {


            // Prepare to show game
            Console.CursorVisible = false;
        

            // Create player
            PlayerCharacter player = new PlayerCharacter('@', ConsoleColor.Green);
            MapLoader mapLoader = new MapLoader();
            player.position = new Point2D(1,1);
            player.Draw();
            bool game_running = true;
            MapLoader loader = new MapLoader();
            Level01 = loader.LoadTestMap();
            Map map = new Map();
            player.Map = Level01.mapTiles;
            player.MapWidth = Level01.mapWidth;

            while (game_running)
            {
                player.Draw();
                Level01.DrawMap();
                player.Draw();
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        player.Move(0, -1);
                        player.Draw();
                        break;
                    case ConsoleKey.DownArrow:
                        player.Move(0, 1);
                        player.Draw();
                        break;
                    case ConsoleKey.LeftArrow:
                        player.Move(-1, 0);
                        player.Draw();
                        break;
                    case ConsoleKey.RightArrow:
                        player.Move(1, 0);
                        player.Draw();
                        break;
                    case ConsoleKey.Home:
                        player.Move(-1, -1);
                        player.Draw();
                        break;
                    case ConsoleKey.PageUp:
                        player.Move(1, -1);
                        player.Draw();
                        break;
                    case ConsoleKey.PageDown:
                        player.Move(1, 1);
                        player.Draw();
                        break;
                    case ConsoleKey.End:
                        player.Move(-1, 1);
                        player.Draw();
                        break;

                    case ConsoleKey.Escape:
                        game_running = false;
                        break;

                    default:
                        break;
                };
            }
        }
    }

}
