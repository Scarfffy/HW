using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace CityParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: CityParser <input_file_path> <output_file_path>");
                return;
            }

            string inputFilePath = args[0];
            string outputFilePath = args[1];

            List<CityInfo> cities = new List<CityInfo>();

            string[] lines = File.ReadAllLines(inputFilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    string[] info = parts[1].Split(';');
                    if (info.Length == 3 && int.TryParse(info[0], out int area) && int.TryParse(info[1], out int population))
                    {
                        cities.Add(new CityInfo
                        {
                            City = parts[0],
                            Area = area,
                            Population = population,
                            Country = info[2]
                        });
                    }
                }
            }

            string json = JsonConvert.SerializeObject(cities, Formatting.Indented);
            File.WriteAllText(outputFilePath, json);

            Console.WriteLine($"Parsed {cities.Count} lines and saved to {outputFilePath}");
        }
    }

    class CityInfo
    {
        public string City { get; set; }
        public int Area { get; set; }
        public int Population { get; set; }
        public string Country { get; set; }
    }
}

