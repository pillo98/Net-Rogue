using ZeroElectric.Vinculum;

namespace Rogue
{
    public enum Race
    {
        Human,
        Elf,
        Orc
    }
    public enum Class
    {
        Rogue,
        Warrior,
        Mage
    }

    public class PlayerCharacter
    {
        public string name;
        public Race race;
        public Class Role;

        public Point2D position;
        public Point2D LastPosition;
        public int NextPosition;
        public int[] Map;
        public int Numero;
        public int MapWidth;

        private char image;
        private Color color;

        public PlayerCharacter(char image, Color color)
        {
            this.image = image;
            this.color = color;
        }
        public void Move(int x_move, int y_move)
        {
            position.x += x_move;
            position.y += y_move;
            NextPosition = position.x + position.y * MapWidth;
            Numero = Map[NextPosition];
            if (Numero != 2 && Numero != 4 && Numero != 3)
            {

                LastPosition.x = position.x - x_move;
                LastPosition.y = position.y - y_move;
                position.x = Math.Clamp(position.x, 0, Console.WindowWidth - 1);
                position.y = Math.Clamp(position.y, 0, Console.WindowHeight - 1);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(LastPosition.x, LastPosition.y);
                Raylib.DrawRectangle(LastPosition.x * Game.tileSize, LastPosition.y * Game.tileSize, Game.tileSize, Game.tileSize, Raylib.GRAY);
                Raylib.DrawText($" ", LastPosition.x * Game.tileSize, LastPosition.y * Game.tileSize, Game.tileSize, color);


            }
            else
            {
                position.x -= x_move;
                position.y -= y_move;
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(position.x, position.y);
            Raylib.DrawRectangle(position.x * Game.tileSize, position.y * Game.tileSize, Game.tileSize, Game.tileSize, Raylib.GRAY);
            Raylib.DrawText($"{image}", position.x * Game.tileSize, position.y * Game.tileSize, Game.tileSize, color);


        }
    }
}