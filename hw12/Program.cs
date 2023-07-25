using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of threads to use: ");
            int numThreads = int.Parse(Console.ReadLine());

            var randomArrayTask = Task.Run(() => RandomArrayTask.GenerateRandomArray(numThreads));
            randomArrayTask.Wait();
            var randomArray = randomArrayTask.Result;
            Console.WriteLine("Random Array: " + string.Join(", ", randomArray));

            var functionArrayTask = Task.Run(() => FunctionArrayTask.GenerateFunctionArray(numThreads));
            functionArrayTask.Wait();
            var functionArray = functionArrayTask.Result;
            Console.WriteLine("Function Array: " + string.Join(", ", functionArray));

            int startIndex = 2;
            int length = 5;
            var copyArrayTask = Task.Run(() => CopyArrayTask.CopyPartOfArray(randomArray, startIndex, length, numThreads));
            copyArrayTask.Wait();
            var copiedArray = copyArrayTask.Result;
            Console.WriteLine("Copied Array: " + string.Join(", ", copiedArray));

            var statisticsTask = Task.Run(() => ArrayStatisticsTask.CalculateStatistics(randomArray, numThreads));
            statisticsTask.Wait();
            var statistics = statisticsTask.Result;
            Console.WriteLine($"Min: {statistics.Min}, Max: {statistics.Max}, Sum: {statistics.Sum}, Average: {statistics.Average}");

            string longText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            var charFrequencyTask = Task.Run(() => FrequencyDictionaryTask.CharacterFrequency(longText, numThreads));
            charFrequencyTask.Wait();
            var charFrequencyDict = charFrequencyTask.Result;
            Console.WriteLine("Character Frequency Dictionary:");
            foreach (var kvp in charFrequencyDict)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            var wordFrequencyTask = Task.Run(() => FrequencyDictionaryTask.WordFrequency(longText, numThreads));
            wordFrequencyTask.Wait();
            var wordFrequencyDict = wordFrequencyTask.Result;
            Console.WriteLine("Word Frequency Dictionary:");
            foreach (var kvp in wordFrequencyDict)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }

    class RandomArrayTask
    {
        public static int[] GenerateRandomArray(int numThreads)
        {
            var random = new Random();
            int[] array = new int[1000000];
            Parallel.For(0, array.Length, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, i =>
            {
                array[i] = random.Next(1000);
            });

            return array;
        }
    }

    class FunctionArrayTask
    {
        public static int[] GenerateFunctionArray(int numThreads)
        {
            int[] array = new int[1000000];
            Parallel.For(0, array.Length, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, i =>
            {
                array[i] = Function(i);
            });

            return array;
        }

        private static int Function(int i)
        {
            return i * i;
        }
    }

    class CopyArrayTask
    {
        public static int[] CopyPartOfArray(int[] array, int startIndex, int length, int numThreads)
        {
            int[] copyArray = new int[length];
            Parallel.For(0, length, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, i =>
            {
                copyArray[i] = array[startIndex + i];
            });

            return copyArray;
        }
    }

    class ArrayStatisticsTask
    {
        public static (int Min, int Max, long Sum, double Average) CalculateStatistics(int[] array, int numThreads)
        {
            int min = array[0];
            int max = array[0];
            long sum = 0;

            Parallel.ForEach(Partitioner.Create(0, array.Length), new ParallelOptions { MaxDegreeOfParallelism = numThreads }, range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    int value = array[i];
                    if (value < min) min = value;
                    if (value > max) max = value;
                    sum += value;
                }
            });

            double average = (double)sum / array.Length;

            return (min, max, sum, average);
        }
    }

    class FrequencyDictionaryTask
    {
        public static Dictionary<char, int> CharacterFrequency(string text, int numThreads)
        {
            var charFrequency = new Dictionary<char, int>();

            Parallel.ForEach(text, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, c =>
            {
                lock (charFrequency)
                {
                    if (charFrequency.ContainsKey(c))
                        charFrequency[c]++;
                    else
                        charFrequency[c] = 1;
                }
            });

            return charFrequency;
        }

        public static Dictionary<string, int> WordFrequency(string text, int numThreads)
        {
            var wordFrequency = new Dictionary<string, int>();
            string[] words = text.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            Parallel.ForEach(words, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, word =>
            {
                lock (wordFrequency)
                {
                    if (wordFrequency.ContainsKey(word))
                        wordFrequency[word]++;
                    else
                        wordFrequency[word] = 1;
                }
            });

            return wordFrequency;
        }
    }
}
