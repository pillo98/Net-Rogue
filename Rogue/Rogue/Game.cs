using System.Threading;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public class Game
    {
        PlayerCharacter player;
        MapS level01;
        public static readonly int tileSize = 16;

        private string AskName()
        {

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
                        

                    }
                }
                if (nameOK == false)
                {
                    Console.WriteLine("Name can only contain letters!");
                    continue;
                }
                return name;
            }
        }

        private Race AskRace()
        {
            while (true)
            {
                Console.WriteLine("Select race");
                Console.WriteLine(Race.Human.ToString());
                Console.WriteLine(Race.Elf.ToString());
                Console.WriteLine(Race.Orc.ToString());
                string race = Console.ReadLine();
                if (race == Race.Human.ToString())
                {
                    return Race.Human;
                }
                else if (race == Race.Elf.ToString())
                {
                    return Race.Elf;
                }
                else if (race == Race.Orc.ToString())
                {
                    return Race.Orc;
                }
                else
                {
                    Console.WriteLine("That is not a Race");
                    continue;
                }
            }
        }

        private Class AskRole() 
        {
            while (true)
            {


                Console.WriteLine("Select Class");
                Console.WriteLine(Class.Rogue.ToString());
                Console.WriteLine(Class.Warrior.ToString());
                Console.WriteLine(Class.Mage.ToString());
                string Role = Console.ReadLine();
                if (Role == Class.Rogue.ToString())
                {
                    return Class.Rogue;
                }
                else if (Role == Class.Warrior.ToString())
                {
                    return Class.Warrior;
                }
                else if (Role == Class.Mage.ToString())
                {
                    return Class.Mage;
                }
                else
                {
                    Console.WriteLine("That is not a Class");
                    continue;
                }
            }
        }

        private PlayerCharacter CreateCharacter() 
        {
            PlayerCharacter player = new PlayerCharacter('@', Raylib.GREEN);
            player.name = AskName();
            player.race = AskRace();
            player.Role = AskRole();
            return player;
        }

        public void Run()
        {
            Console.Clear();
            Init();
            GameLoop();
        }

        private void Init()
        {
            player = CreateCharacter();
            MapLoader loader = new MapLoader();
            level01 = loader.LoadMapFromFile("Maps/mapfile.json");
            player.Map = level01.layers[0].mapTiles;
            player.MapWidth = level01.mapWidth;
            player.position = new Point2D(1, 1);
            Console.Clear();
            Raylib.InitWindow(480, 270, "Rogue");
            Raylib.SetTargetFPS(30);
        }

        private void DrawGame()
        {
            Raylib.BeginDrawing();

            level01.DrawMap();
            player.Draw();

            Raylib.EndDrawing();
        }

        private void UpdateGame()
        {
            Console.CursorVisible = false;
            if (Console.KeyAvailable == false)
            {
                System.Threading.Thread.Sleep(33);
                return;
            }

               ConsoleKeyInfo key = Console.ReadKey();
               switch (key.Key)
               {
                    case ConsoleKey.UpArrow:
                        player.Move(0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        player.Move(0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        player.Move(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        player.Move(1, 0);
                        break;
                    case ConsoleKey.Home:
                        player.Move(-1, -1);
                        break;
                    case ConsoleKey.PageUp:
                        player.Move(1, -1);
                        break;
                    case ConsoleKey.PageDown:
                        player.Move(1, 1);
                        break;
                    case ConsoleKey.End:
                        player.Move(-1, 1);
                        break;

                    default:
                        break;
               };
        }
        private void GameLoop()
        {
            while (true)
            {
                UpdateGame();
                DrawGame();

            }

        }
    }

}
