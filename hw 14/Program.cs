using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTasksWithCancellation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Enter the number of tasks to use: ");
            int numTasks = int.Parse(Console.ReadLine());

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var progressTracker = new ProgressTracker(numTasks);

            Console.WriteLine("Press ESC to cancel...");

            try
            {
                var cancellationTask = Task.Run(() =>
                {
                    while (true)
                    {
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            cancellationTokenSource.Cancel();
                            break;
                        }
                    }
                });

                await cancellationTask;

                var randomArrayTask = RandomArrayTask.GenerateRandomArrayAsync(numTasks, progressTracker, cancellationToken);
                var randomArray = await randomArrayTask;
                Console.WriteLine("Random Array: " + string.Join(", ", randomArray));

                var functionArrayTask = FunctionArrayTask.GenerateFunctionArrayAsync(numTasks, progressTracker, cancellationToken);
                var functionArray = await functionArrayTask;
                Console.WriteLine("Function Array: " + string.Join(", ", functionArray));

                var copiedArrayTask = CopyArrayTask.CopyPartOfArrayAsync(functionArray, 100000, 100, numTasks, progressTracker, cancellationToken);
                var copiedArray = await copiedArrayTask;
                Console.WriteLine("Copied Array: " + string.Join(", ", copiedArray));

                var statisticsTask = ArrayStatisticsTask.CalculateStatisticsAsync(randomArray, numTasks, progressTracker, cancellationToken);
                var statistics = await statisticsTask;
                Console.WriteLine($"Statistics: Min={statistics.Min}, Max={statistics.Max}, Sum={statistics.Sum}, Average={statistics.Average}");

                var charFrequencyTask = FrequencyDictionaryTask.CharacterFrequencyAsync("Hello, Parallel Programming!", numTasks, progressTracker, cancellationToken);
                var charFrequency = await charFrequencyTask;
                Console.WriteLine("Character Frequency: ");
                foreach (var kvp in charFrequency)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }

                var wordFrequencyTask = FrequencyDictionaryTask.WordFrequencyAsync("Hello, Parallel Programming! Hello, Parallel Programming!", numTasks, progressTracker, cancellationToken);
                var wordFrequency = await wordFrequencyTask;
                Console.WriteLine("Word Frequency: ");
                foreach (var kvp in wordFrequency)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Execution was cancelled.");
            }
        }
    }

    class ProgressTracker
    {
        private readonly object lockObject = new object();
        private readonly int[] progressArray;

        public ProgressTracker(int numTasks)
        {
            progressArray = new int[numTasks];
        }

        public void ReportProgress(int taskIndex, int processedCount)
        {
            lock (lockObject)
            {
                progressArray[taskIndex] = processedCount;
                Console.CursorLeft = 0;
                Console.Write("Progress: " + string.Join(", ", progressArray));
            }
        }
    }

    class RandomArrayTask
    {
        public static async Task<int[]> GenerateRandomArrayAsync(int numTasks, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            var random = new Random();
            int[] array = new int[1000000];

            var tasks = Enumerable.Range(0, array.Length).Select(async i =>
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                array[i] = random.Next(1000);
                progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, i + 1);
            });

            await Task.WhenAll(tasks);

            return array;
        }
    }

    class FunctionArrayTask
    {
        public static async Task<int[]> GenerateFunctionArrayAsync(int numTasks, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            int[] array = new int[1000000];

            var tasks = Enumerable.Range(0, array.Length).Select(async i =>
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                array[i] = Function(i);
                progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, i + 1);
            });

            await Task.WhenAll(tasks);

            return array;
        }

        private static int Function(int i)
        {
            return i * i;
        }
    }

    class CopyArrayTask
    {
        public static async Task<int[]> CopyPartOfArrayAsync(int[] array, int startIndex, int length, int numTasks, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            int[] copyArray = new int[length];

            var tasks = Enumerable.Range(0, length).Select(async i =>
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                copyArray[i] = array[startIndex + i];
                progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, i + 1);
            });

            await Task.WhenAll(tasks);

            return copyArray;
        }
    }

    class ArrayStatisticsTask
    {
        public static async Task<(int Min, int Max, long Sum, double Average)> CalculateStatisticsAsync(int[] array, int numTasks, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            int min = array[0];
            int max = array[0];
            long sum = 0;

            var tasks = Enumerable.Range(0, numTasks).Select(async i =>
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                var startIndex = i * (array.Length / numTasks);
                var endIndex = i == numTasks - 1 ? array.Length : (i + 1) * (array.Length / numTasks);
                for (int j = startIndex; j < endIndex; j++)
                {
                    int value = array[j];
                    if (value < min) min = value;
                    if (value > max) max = value;
                    sum += value;

                    progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, j + 1);
                }
            });

            await Task.WhenAll(tasks);

            double average = (double)sum / array.Length;

            return (min, max, sum, average);
        }
    }

    class FrequencyDictionaryTask
    {
        public static async Task<Dictionary<char, int>> CharacterFrequencyAsync(string text, int numTasks, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            var charFrequency = new Dictionary<char, int>();

            var tasks = text.Select(async (c, index) =>
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                await Task.Yield();

                lock (charFrequency)
                {
                    if (charFrequency.ContainsKey(c))
                        charFrequency[c]++;
                    else
                        charFrequency[c] = 1;
                }

                progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, index + 1);
            });

            await Task.WhenAll(tasks);

            return charFrequency;
        }

        public static async Task<Dictionary<string, int>> WordFrequencyAsync(string text, int numTasks, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            var wordFrequency = new Dictionary<string, int>();
            string[] words = text.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            var tasks = words.Select(async (word, index) =>
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                await Task.Yield();

                lock (wordFrequency)
                {
                    if (wordFrequency.ContainsKey(word))
                        wordFrequency[word]++;
                    else
                        wordFrequency[word] = 1;
                }

                progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, wordFrequency.Values.Sum());
            });

            await Task.WhenAll(tasks);

            return wordFrequency;
        }
    }
}
