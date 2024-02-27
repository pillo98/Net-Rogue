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
            int mapWidth = 8;
            int[] mapTiles = new int[] {
                2, 3, 3, 3, 3, 3, 3, 2,
                4, 1, 1, 2, 1, 1, 1, 4,
                4, 1, 1, 2, 1, 1, 1, 4,
                4, 1, 1, 1, 1, 1, 2, 4,
                4, 2, 2, 2, 1, 1, 1, 4,
                4, 1, 1, 1, 1, 1, 1, 4,
                2, 3, 3, 3, 3, 3, 3, 2 }
            ;
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
