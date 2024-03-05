using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    internal class MapS
    {
        public int mapWidth;
        public MapLayer[] layers;

        List<Enemy> enemies;
        List<Item> items;

        public struct Enemy
        {
            public string name;
            public Point2D position;
            public Enemy(string pname, Point2D pposition)
            {
                this.name = pname;
                this.position = pposition;
            }

        }

        public struct Item
        {
            public string name;
            public Point2D position;
            public Item(string pname, Point2D pposition)
            {
                this.name = pname;
                this.position = pposition;
            }

        }

        public void DrawMap()
        {
            MapLayer groundLayer = layers[0];

            int[] groundTiles = groundLayer.mapTiles;
            Console.ForegroundColor = ConsoleColor.Gray;
            int mapHeight = groundTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int index = x + y * mapWidth;
                    int tileId = groundTiles[index];
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
        public void DrawEnemies()
        {
            MapLayer enemyLayer = layers[2];

            int[] enemyTiles = enemyLayer.mapTiles;
            int mapHeight = enemyTiles.Length / mapWidth;
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int index = x + y * mapWidth;
                    int tileId = enemyTiles[index];
                    Console.SetCursorPosition(x, y);
                    switch (tileId)
                    {
                        case 1:
                            Console.Write("%");
                            break;
                        case 2:
                            Console.Write("%");
                            break;
                        case 3:
                            Console.Write("%");
                            break;
                        case 4:
                            Console.Write("%");
                            break;
                        default:
                            //nothing
                            break;
                    }
                }
            }
        }
        public void DrawItems()
        {

            MapLayer groundLayer = layers[1];

            int[] groundTiles = groundLayer.mapTiles;
            Console.ForegroundColor = ConsoleColor.Gray;
            int mapHeight = groundTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int index = x + y * mapWidth;
                    int tileId = groundTiles[index];
                    Console.SetCursorPosition(x, y);
                    switch (tileId)
                    {
                        case 1:
                            Console.Write("$");
                            break;
                        case 2:
                            Console.Write("$");
                            break;
                        case 3:
                            Console.Write("$");
                            break;
                        case 4:
                            Console.Write("$");
                            break;
                        default:
                            //nothing
                            break;
                    }
                }
            }
        }

        public void LoadEnemiesAndItems()
        {
            enemies = new List<Enemy>();


            MapLayer enemyLayer = layers[2];

            int[] enemyTiles = enemyLayer.mapTiles;
            int mapHeight = enemyTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Point2D position = new Point2D(x, y);
                    int index = x + y * mapWidth;
                    int tileId = enemyTiles[index];
                    switch (tileId)
                    {
                        case 0:
                            // ei mitään tässä kohtaa
                            break;
                        case 1:
                            enemies.Add(new Enemy("Orc", position));
                            break;
                        case 2:
                            enemies.Add(new Enemy("Goblin", position));
                            break;
                        case 3:
                            enemies.Add(new Enemy("Wraith", position));
                            break;
                        case 4:
                            enemies.Add(new Enemy("Bandit", position));
                            break;
                    }
                }
            }


            items = new List<Item>();

            MapLayer itemLayer = layers[1];

            int[] itemTiles = itemLayer.mapTiles;
            int mapHeight1 = itemTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Point2D position = new Point2D(x, y);
                    int index = x + y * mapWidth;
                    int tileId = enemyTiles[index];
                    switch (tileId)
                    {
                        case 0:
                            // ei mitään tässä kohtaa
                            break;
                        case 1:
                            items.Add(new Item("Sword", position));
                            break;
                        case 2:
                            items.Add(new Item("Spear", position));
                            break;
                        case 3:
                            items.Add(new Item("Ring", position));
                            break;
                        case 4:
                            items.Add(new Item("Armor", position));
                            break;
                        case 5:
                            items.Add(new Item("Shield", position));
                            break;
                        case 6:
                            items.Add(new Item("Helmet", position));
                            break;
                    }
                }
            }
        }

    }
}
