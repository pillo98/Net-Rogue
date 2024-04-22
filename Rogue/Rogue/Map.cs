using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;
using static System.Net.Mime.MediaTypeNames;

namespace Rogue
{
    internal class MapS
    {
        public int mapWidth;
        public MapLayer[] layers;

        public enum MapTile : int
        {
            Floor = 5,
            Wall = 8
        }


        List<Enemy> enemies;
        List<Item> items;

        public Tile Floor;
        public Tile Wall;
        public struct Tile
        {
            public Texture Image { get; set; }
            public float imagePixelX;
            public float imagePixelY;
            public Rectangle imageRect;
            public int index;
        }


        public struct Enemy
        {
            public string name;
            public Point2D position;
            public Texture EnemySprite;
            public Enemy(string pname, Point2D pposition, Texture Sprite)
            {
                this.name = pname;
                this.position = pposition;
                this.EnemySprite = Sprite;
            }

        }
        public struct Item
        {
            public string name;
            public Point2D position;
            public Item(string pname, Point2D pposition, Texture item_image)
            {
                this.name = pname;
                this.position = pposition;
            }

        }
        public Tile SetImageAndIndex(Tile tile, int index)
        {
            float tileSize = 17.2f;
            tile.Image = Game.atlasImage;
            tile.index = index;
            tile.imagePixelX = (tile.index % 12) * tileSize;
            tile.imagePixelY = (int)(tile.index / 12) * tileSize;

            tile.imageRect = new Rectangle(tile.imagePixelX, tile.imagePixelY, Game.tileSize, Game.tileSize);
            return tile;
        }
        public void DrawMap()
        {
            
            MapLayer groundLayer = layers[0];
            Floor = SetImageAndIndex(Floor, 0);
            Wall = SetImageAndIndex(Wall, 14);
            int[] groundTiles = groundLayer.mapTiles;
            Console.ForegroundColor = ConsoleColor.Gray;
            int mapHeight = groundTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int tileId = groundTiles[x + y * mapWidth];
                    Vector2 pos = new Vector2(x * Game.tileSize, y * Game.tileSize);
                    switch (tileId) 
                    { 
                        case 0: 
                            break;
                        case 5:
                            Raylib.DrawTextureRec(Floor.Image, Floor.imageRect, pos, Raylib.WHITE);
                            break;
                        case 8:
                            Raylib.DrawTextureRec(Wall.Image, Wall.imageRect, pos, Raylib.WHITE);
                            break;
                    }
                }
            }
            LoadEnemiesAndItems();
        }
        public void DrawEnemy(Texture Sprite, Point2D position)
        {
            int imagesPerRow = 12;
            float tileSize = 16.9f;
            
            int atlasIndex = 0 + 10 * imagesPerRow;

            int imageX = atlasIndex % imagesPerRow; 
            int imageY = (int)(atlasIndex / imagesPerRow);
            float imagePixelX = imageX * tileSize;
            float imagePixelY = imageY * tileSize;


            Rectangle imageRect = new Rectangle(imagePixelX, imagePixelY, Game.tileSize, Game.tileSize);

            // Laske paikka ruudulla
            int pixelPositionX = position.x * Game.tileSize;
            int pixelPositionY = position.y * Game.tileSize;
            Vector2 pixelPosition = new Vector2(pixelPositionX, pixelPositionY);
            Raylib.DrawTextureRec(Sprite, imageRect, pixelPosition, Raylib.WHITE);
        }
        public void DrawItem(Texture Sprite, Point2D position)
        {
            int imagesPerRow = 12;
            float tileSize = 16.9f;

            int atlasIndex = 1 + 5 * imagesPerRow;

            int imageX = atlasIndex % imagesPerRow;
            int imageY = (int)(atlasIndex / imagesPerRow);
            float imagePixelX = imageX * tileSize;
            float imagePixelY = imageY * tileSize;


            Rectangle imageRect = new Rectangle(imagePixelX, imagePixelY, Game.tileSize, Game.tileSize);

            // Laske paikka ruudulla
            int pixelPositionX = position.x * Game.tileSize;
            int pixelPositionY = position.y * Game.tileSize;
            Vector2 pixelPosition = new Vector2(pixelPositionX, pixelPositionY);
            Raylib.DrawTextureRec(Sprite, imageRect, pixelPosition, Raylib.WHITE);

        }
        public void LoadEnemiesAndItems()
        {
            enemies = new List<Enemy>();


            MapLayer enemyLayer = layers[2];
            int[] enemyTiles = enemyLayer.mapTiles;
            int mapHeight = enemyTiles.Length / mapWidth;
            Texture enemy_image = Game.atlasImage;
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
                            enemies.Add(new Enemy("Orc", position, enemy_image));
                            DrawEnemy(enemy_image, position);
                            break;
                        case 2:
                            enemies.Add(new Enemy("Goblin", position, enemy_image));
                            DrawEnemy(enemy_image, position);
                            break;
                        case 3:
                            enemies.Add(new Enemy("Wraith", position, enemy_image));
                            DrawEnemy(enemy_image, position);
                            break;
                        case 4:
                            enemies.Add(new Enemy("Bandit", position, enemy_image));
                            DrawEnemy(enemy_image, position);
                            break;
                    }
                }
            }


            items = new List<Item>();

            MapLayer itemLayer = layers[1];
            Texture item_image = Game.atlasImage;
            int[] itemTiles = itemLayer.mapTiles;
            int mapHeight1 = itemTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight1; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Point2D position = new Point2D(x, y);
                    int index = x + y * mapWidth;
                    int tileId = itemTiles[index];
                    switch (tileId)
                    {

                        case 0:
                            // ei mitään tässä kohtaa
                            break;
                        case 1:
                            items.Add(new Item("Sword", position, item_image));
                            DrawItem(item_image, position);
                            break;
                        case 2:
                            items.Add(new Item("Spear", position, item_image));
                            DrawItem(item_image, position);
                            break;
                        case 3:
                            items.Add(new Item("Ring", position, item_image));
                            DrawItem(item_image, position);
                            break;
                        case 4:
                            items.Add(new Item("Armor", position, item_image));
                            DrawItem(item_image, position);
                            break;
                        case 5:
                            items.Add(new Item("Shield", position, item_image));
                            DrawItem(item_image, position);
                            break;
                        case 6:
                            items.Add(new Item("Helmet", position, item_image));
                            DrawItem(item_image, position);
                            break;

                    }
                }
            }
        }
    }
}
