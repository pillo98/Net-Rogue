using System.Numerics;
using System.Threading;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public class Game
    {
        int screen_width = 1280;
        int screen_height = 720;

        PlayerCharacter player;
        MapS level01;
        public static readonly int tileSize = 16;
        int game_width;
        int game_height;
        RenderTexture game_screen;
        public static Texture atlasImage;
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
            PlayerCharacter player = new PlayerCharacter();
            player.name = AskName();
            player.race = AskRace();
            player.Role = AskRole();
            return player;
        }

        public void Run()
        {
            Init();
            GameLoop();
            Raylib.UnloadRenderTexture(game_screen);
            Raylib.CloseWindow();
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

            Raylib.SetTargetFPS(30);
            Raylib.InitWindow(screen_width, screen_height, "Rogue");
            Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);

            atlasImage = Raylib.LoadTexture("Images/MAP.png");

            player.SetImageAndIndex(atlasImage, 12, 96);

            game_width = 480;
            game_height = 270;

            game_screen = Raylib.LoadRenderTexture(game_width, game_height);
            Raylib.SetTextureFilter(game_screen.texture, TextureFilter.TEXTURE_FILTER_BILINEAR);
            Raylib.SetWindowMinSize(game_width, game_height);

        }

        private void DrawGame()
        {
            Raylib.BeginDrawing();

            level01.DrawMap();
            player.Draw();

            Raylib.EndDrawing();
        }

        private void DrawGameToTexture()
        {
            Raylib.BeginTextureMode(game_screen);
            DrawGame();
            Raylib.EndTextureMode();
            DrawGameScaled();


        }

        private void UpdateGame()
        {
            Console.CursorVisible = false;
            if (Console.KeyAvailable == false)
            {
                Thread.Sleep(33);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
            {
                player.Move(0, -1);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
            {
                player.Move(0, 1);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                player.Move(1, 0);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                player.Move(-1, 0);
            }
        }

        private void GameLoop()
        {
            while (Raylib.WindowShouldClose() == false)
            {
                UpdateGame();
                DrawGameToTexture();

            }

        }

        private void DrawGameScaled()
        {

            // Tässä piirretään tekstuuri ruudulle
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.DARKGRAY);

            int draw_width = Raylib.GetScreenWidth();
            int draw_height = Raylib.GetScreenHeight();
            float scale = Math.Min((float)draw_width / game_width, (float)draw_height / game_height);

            // Note: when drawing on texture, the Y-axis is
            //flipped, need to multiply height by -1
            Rectangle source = new Rectangle(0.0f, 0.0f,
                game_screen.texture.width,
                game_screen.texture.height * -1.0f);

            Rectangle destination = new Rectangle(
                (draw_width - (float)game_width * scale) * 0.5f,
                (draw_height - (float)game_height * scale) * 0.5f,
                game_width * scale,
                game_height * scale);

            Raylib.DrawTexturePro(game_screen.texture,
                source, destination,
                new Vector2(0, 0), 0.0f, Raylib.WHITE);

            Raylib.EndDrawing();
        }
    }

}
