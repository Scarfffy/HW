using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTasksWithCancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of threads to use: ");
            int numThreads = int.Parse(Console.ReadLine());

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var progressTracker = new ProgressTracker(numThreads);

            Console.WriteLine("Press ESC to cancel...");

            try
            {
                Task.Run(() =>
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

                var randomArrayTask = Task.Run(() => RandomArrayTask.GenerateRandomArray(numThreads, progressTracker, cancellationToken));
                randomArrayTask.Wait();
                var randomArray = randomArrayTask.Result;
                Console.WriteLine("Random Array: " + string.Join(", ", randomArray));

                // ... аналогічно оновіть решту завдань
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

        public ProgressTracker(int numThreads)
        {
            progressArray = new int[numThreads];
        }

        public void ReportProgress(int threadIndex, int processedCount)
        {
            lock (lockObject)
            {
                progressArray[threadIndex] = processedCount;
                Console.CursorLeft = 0;
                Console.Write("Progress: " + string.Join(", ", progressArray));
            }
        }
    }

    class RandomArrayTask
    {
        public static int[] GenerateRandomArray(int numThreads, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            var random = new Random();
            int[] array = new int[1000000];

            Parallel.For(0, array.Length, new ParallelOptions { MaxDegreeOfParallelism = numThreads, CancellationToken = cancellationToken }, (i, state) =>
            {
                array[i] = random.Next(1000);
                progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, i + 1);

                if (cancellationToken.IsCancellationRequested)
                    state.Stop();
            });

            return array;
        }
    }

    class FunctionArrayTask
    {
        public static int[] GenerateFunctionArray(int numThreads, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            int[] array = new int[1000000];

            Parallel.For(0, array.Length, new ParallelOptions { MaxDegreeOfParallelism = numThreads, CancellationToken = cancellationToken }, (i, state) =>
            {
                array[i] = Function(i);
                progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, i + 1);

                if (cancellationToken.IsCancellationRequested)
                    state.Stop();
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
        public static int[] CopyPartOfArray(int[] array, int startIndex, int length, int numThreads, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            int[] copyArray = new int[length];

            Parallel.For(0, length, new ParallelOptions { MaxDegreeOfParallelism = numThreads, CancellationToken = cancellationToken }, (i, state) =>
            {
                copyArray[i] = array[startIndex + i];
                progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, i + 1);

                if (cancellationToken.IsCancellationRequested)
                    state.Stop();
            });

            return copyArray;
        }
    }

    class ArrayStatisticsTask
    {
        public static (int Min, int Max, long Sum, double Average) CalculateStatistics(int[] array, int numThreads, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            int min = array[0];
            int max = array[0];
            long sum = 0;

            Parallel.ForEach(Partitioner.Create(0, array.Length), new ParallelOptions { MaxDegreeOfParallelism = numThreads, CancellationToken = cancellationToken }, (range, state) =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    int value = array[i];
                    if (value < min) min = value;
                    if (value > max) max = value;
                    sum += value;

                    progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, i + 1);

                    if (cancellationToken.IsCancellationRequested)
                        state.Stop();
                }
            });

            double average = (double)sum / array.Length;

            return (min, max, sum, average);
        }
    }

    class FrequencyDictionaryTask
    {
        public static Dictionary<char, int> CharacterFrequency(string text, int numThreads, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            var charFrequency = new Dictionary<char, int>();

            Parallel.ForEach(text, new ParallelOptions { MaxDegreeOfParallelism = numThreads, CancellationToken = cancellationToken }, (c, state, index) =>
            {
                if (cancellationToken.IsCancellationRequested)
                    state.Stop();

                lock (charFrequency)
                {
                    if (charFrequency.ContainsKey(c))
                        charFrequency[c]++;
                    else
                        charFrequency[c] = 1;
                }

                progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, (int)index + 1);
            });

            return charFrequency;
        }

        public static Dictionary<string, int> WordFrequency(string text, int numThreads, ProgressTracker progressTracker, CancellationToken cancellationToken)
        {
            var wordFrequency = new Dictionary<string, int>();
            string[] words = text.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            Parallel.ForEach(words, new ParallelOptions { MaxDegreeOfParallelism = numThreads, CancellationToken = cancellationToken }, (word, state, index) =>
            {
                lock (wordFrequency)
                {
                    if (wordFrequency.ContainsKey(word))
                        wordFrequency[word]++;
                    else
                        wordFrequency[word] = 1;
                }

                progressTracker.ReportProgress(Thread.CurrentThread.ManagedThreadId, (int)index + 1);

                if (cancellationToken.IsCancellationRequested)
                    state.Stop();
            });

            return wordFrequency;
        }
    }


}
