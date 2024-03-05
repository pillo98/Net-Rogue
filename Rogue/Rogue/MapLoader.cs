using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
namespace Rogue
{
    internal class MapLoader
    {

        public MapS LoadTestMap()
        { 
            MapS test = new MapS();
            test.mapWidth = 8;
            test.layers[1].mapTiles = new int[] {
            2, 2, 2, 2, 2, 2, 2, 2,
            2, 1, 1, 2, 1, 1, 1, 2,
            2, 1, 1, 2, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 2, 2,
            2, 2, 2, 2, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 1, 2,
            2, 2, 2, 2, 2, 2, 2, 2 };
            return test;

        }
        public void TestFileReading (string filename)
        {
            using (StreamReader reader = File.OpenText(filename))
            {
                Console.WriteLine("File contents:");
                Console.WriteLine();

                string line;
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null)
                    {
                        break; // End of file
                    }
                    Console.WriteLine(line);
                }
            }
        }
        
        public MapS LoadMapFromFile(string filename) 
        {
            MapS map = new MapS();
            bool fileFound = File.Exists(filename);
            if (fileFound == false)
            {
                Console.WriteLine($"File {filename} not found");
                return LoadTestMap(); // Return the test map as fallback
            }
            using (StreamReader reader = File.OpenText(filename))
            {
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<MapS>(fileContents);
            }
        }
    }
}
