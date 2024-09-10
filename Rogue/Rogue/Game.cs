using System.Numerics;
using System.Threading;
using ZeroElectric.Vinculum;
using TurboMapReader;
using RayGuiCreator;
using System.Xml.Linq;


namespace Rogue
{
    public class Game
    {
        int screen_width = 1280;
        int screen_height = 720;

        PlayerCharacter player;
        Map level01;
        TiledMap level;
        public static readonly int tileSize = 16;
        int game_width;
        int game_height;
        RenderTexture game_screen;
        public static Sound EnemySound;
        public static Sound ItemSound;
        public static Sound WallCollide;
        public static Texture atlasImage;

        enum GameState
        {
            MainMenu,
            CharacterCreation,
            GameLoop
        }

        GameState currentGameState;
        TextBoxEntry playerNameEntry;
        bool IsNameOk = false;
        public MultipleChoiceEntry classChoices = new MultipleChoiceEntry(new string[] {Class.Rogue.ToString(), Class.Warrior.ToString(), Class.Mage.ToString()});
        public MultipleChoiceEntry raceChoices = new MultipleChoiceEntry(new string[] { Race.Human.ToString(),Race.Elf.ToString(), Race.Orc.ToString()});
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
                player.name = AskName();
                player.race = AskRace();
                player.Role = AskRole();
            }
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
            currentGameState = GameState.MainMenu;
            player = new PlayerCharacter();
            playerNameEntry = new TextBoxEntry(12);

            player.position = new Point2D(2, 2);
            Console.Clear();

            Raylib.InitAudioDevice();
            Raylib.InitWindow(screen_width, screen_height, "Rogue");
            Raylib.SetTargetFPS(30);
            Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);

            EnemySound = Raylib.LoadSound("Sounds/Enemy.mp3");
            ItemSound = Raylib.LoadSound("Sounds/Item.mp3");
            WallCollide = Raylib.LoadSound("Sounds/WallCollide.mp3");



            MapLoader Reader = new MapLoader();
            level01 = Reader.LoadLayeredMap("Maps/mapfile.json");
            level = MapReader.LoadMapFromFile("Maps/mapfile.json");

            Reader.ToMap(level01, level);

            atlasImage = Raylib.LoadTexture("Images/MAP.png");

            player.SetImageAndIndex(atlasImage, 12, 96);

            game_width = 480;
            game_height = 360;

            player.MapWidth = level01.mapWidth;
            player.MapTiles = level01.layers[0].mapTiles;

            game_screen = Raylib.LoadRenderTexture(game_width, game_height);
            Raylib.SetTextureFilter(game_screen.texture, TextureFilter.TEXTURE_FILTER_POINT);
            Raylib.SetWindowMinSize(game_width, game_height);
        }

        private void DrawGame()
        {
            level01.DrawMap();
            player.Draw();



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

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
            {
                player.Move(0, -1);
                level01.GetEnemyAt(player.position);
                level01.GetItemAt(player.position);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
            {
                player.Move(0, 1);
                level01.GetEnemyAt(player.position);
                level01.GetItemAt(player.position);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                player.Move(1, 0);
                level01.GetEnemyAt(player.position);
                level01.GetItemAt(player.position);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                player.Move(-1, 0);
                level01.GetEnemyAt(player.position);
                level01.GetItemAt(player.position);

            }
        }

        private void GameLoop()
        {
            while (Raylib.WindowShouldClose() == false)
            {
                switch (currentGameState)
                {
                    case GameState.MainMenu:
                        DrawMainMenu();
                        break;
                    case GameState.CharacterCreation:
                        DrawCharacterMenu();
                        break;

                    case GameState.GameLoop:
                        UpdateGame();
                        DrawGameToTexture();
                        break;
                }

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

        public void DrawMainMenu()
        {
            // Tyhjennä ruutu ja aloita piirtäminen
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            // Laske ylimmän napin paikka ruudulla.
            int button_width = 100;
            int button_height = 20;
            int button_x = Raylib.GetScreenWidth() / 2 - button_width / 2;
            int button_y = Raylib.GetScreenHeight() / 2 - button_height / 2;

            // Piirrä pelin nimi nappien yläpuolelle
            RayGui.GuiLabel(new Rectangle(button_x, button_y - button_height * 3, button_width, button_height), "Rogue");
            RayGui.GuiLabel(new Rectangle(button_x, button_y - button_height * 2, button_width, button_height), "Move with arrow keys");
            if (RayGui.GuiButton(new Rectangle(button_x, button_y, button_width, button_height), "Start Game") == 1)
            {
                currentGameState = GameState.GameLoop;
            }

            button_y += button_height * 2;
            if (RayGui.GuiButton(new Rectangle(button_x, button_y, button_width, button_height), "Create Character") == 1)
            {
                currentGameState = GameState.CharacterCreation;
            }

            button_y += button_height * 2;
            if (RayGui.GuiButton(new Rectangle(button_x, button_y, button_width, button_height), "Quit") == 1)
            {
                Raylib.CloseWindow();
            }

            Raylib.EndDrawing();
        }

        public void DrawCharacterMenu()
        {
            
            // Tyhjennä ruutu ja aloita piirtäminen
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            int x = 0;
            int y = 40;
            int width = 140;
            MenuCreator c = new MenuCreator(Raylib.GetScreenWidth() / 2 - width / 2, y, width);
            string playername;
            c.Label("Character creation");
            c.Label("");
            c.Label("Character Name:");
            c.TextBox(playerNameEntry);
            c.Label("");
            c.Label("Character Race:");
            c.ToggleGroup(raceChoices);
            c.Label("");
            c.Label("Character Class");
            c.ToggleGroup(classChoices);
            c.Label("");
            c.Label("");
            if(c.LabelButton("Create Character"))
            {
                playername = playerNameEntry.bytes.ToString();

                CheckIfNameOK(playername, IsNameOk);
                if (IsNameOk == true) 
                {
                    player.name = playername;
                    switch (raceChoices.GetSelected())
                    {
                        case "Human":
                            player.race = Race.Human; 
                            break;
                        case "Elf":
                            player.race = Race.Elf;
                            break;
                        case "Orc":
                            player.race = Race.Orc;
                            break;

                    }
                    switch (classChoices.GetSelected())
                    {
                        case "Rogue":
                            player.Role = Class.Rogue;
                            break;
                        case "Warrior":
                            player.Role = Class.Warrior;
                            break;
                        case "Mage":
                            player.Role = Class.Mage;
                            break;

                    }
                    currentGameState = GameState.GameLoop;

                }
                
            }
            c.EndMenu();

            

            Raylib.EndDrawing();
        }

        public string CheckIfNameOK(string name, bool IsNameOK)
        {
            if (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("Not acceptable");
            }
            bool nameOK = true;
            for (int i = 0; i < name.Length; i++)
            {
                char kirjain = name[i];
                if (Char.IsLetter(kirjain) != true)
                {
                    nameOK = false;
                }
            }
            if (nameOK == false)
            {
                Console.WriteLine("Name can only contain letters!");
            }
            IsNameOK = nameOK;
            return name;

        }


    }

}
