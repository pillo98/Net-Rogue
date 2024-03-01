using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    internal class Map
    {
        public int mapWidth;
        public int[] mapTiles;
        public void DrawMap()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            int mapHeight = mapTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int index = x + y * mapWidth;
                    int tileId = mapTiles[index];
                    Console.SetCursorPosition(x, y);
                    switch (tileId)
                    {
                        case 1:
                            Console.Write(".");
                            break;
                        case 2:
                            Console.Write("#");
                            break;
                        case 3:
                            Console.Write("═");
                            break;
                        case 4:
                            Console.Write("║");
                            break;
                        default:
                            Console.Write(" ");
                            break;
                    }
                }
            }
        }
    }
}
