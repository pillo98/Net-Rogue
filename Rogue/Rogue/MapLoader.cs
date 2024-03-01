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

        public Map LoadTestMap()
        { 
            Map test = new Map();
            test.mapWidth = 8;
            test.mapTiles = new int[] {
                2, 3, 3, 3, 3, 3, 3, 2,
                4, 1, 1, 2, 1, 1, 1, 4,
                4, 1, 1, 2, 1, 1, 1, 4,
                4, 1, 1, 1, 1, 1, 2, 4,
                4, 2, 2, 2, 1, 1, 1, 4,
                4, 1, 1, 1, 1, 1, 1, 4,
                2, 3, 3, 3, 3, 3, 3, 2 };
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
        
        public Map LoadMapFromFile(string filename) 
        {
            bool fileFound = File.Exists(filename);
            if (fileFound == false)
            {
                Console.WriteLine($"File {filename} not found");
                return LoadTestMap(); // Return the test map as fallback
            }

            string fileContents;

            using (StreamReader reader = File.OpenText(filename))
            {
                fileContents = reader.ReadToEnd();
            }

            Map loadedMap = JsonConvert.DeserializeObject<Map>(fileContents);

            return loadedMap;
        }
    }
}
