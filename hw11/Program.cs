using System;
using System.IO;
using Newtonsoft.Json;

namespace CityGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: CityGenerator <output_file_path> <number_of_lines>");
                return;
            }

            string namesFilePath = "C:\\Users\\Scarfy\\source\\repos\\hw2\\hw11\\city.txt";
            string outputFilePath = args[0];
            int numberOfLines = int.Parse(args[1]);

            string[] names = File.ReadAllLines(namesFilePath);

            Random random = new Random();
            using (StreamWriter file = new StreamWriter(outputFilePath))
            {
                for (int i = 0; i < numberOfLines; i++)
                {
                    string city = names[random.Next(names.Length)];
                    int area = random.Next(100, 1000);
                    int population = random.Next(1000, 1000000);
                    string country = names[random.Next(names.Length)];
                    string line = $"{city}:{area};{population};{country}";

                    file.WriteLine(line);
                }
            }

            Console.WriteLine($"Generated {numberOfLines} lines and saved to {outputFilePath}");
        }
    }
}
