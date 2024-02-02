using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
    internal class PlayerCharacterS
    {
        public string name;
        public Race race;
        public Class Role;
    }
    public class PlayerCharacter
    {
        public Point2D position;
        public Point2D LastPosition;
        public int NextPosition;
        public int[] Map;
        public int Numero;
        public int MapWidth;

        private char image;
        private ConsoleColor color;

        public PlayerCharacter(char image, ConsoleColor color)
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
            if (Numero != 2)
            {

                LastPosition.x = position.x - x_move;
                LastPosition.y = position.y - y_move;
                position.x = Math.Clamp(position.x, 0, Console.WindowWidth - 1);
                position.y = Math.Clamp(position.y, 0, Console.WindowHeight - 1);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(LastPosition.x, LastPosition.y);
                Console.Write(" ");


            }
        }

        public void Draw()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(position.x, position.y);
            Console.Write(image);


        }
    }
}