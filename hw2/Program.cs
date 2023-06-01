using System.Text;
using System;
using System.Threading;
using System.Security.Cryptography;
using System.IO;
//1
static string ReverseString(string input)
{
    char[] characters = input.ToCharArray();
    int length = input.Length;
    for (int i = 0; i < length / 2; i++)
    {
        char temp = characters[i];
        characters[i] = characters[length - 1 - i];
        characters[length - 1 - i] = temp;
    }
    return new string(characters);
}

Console.WriteLine("write text: ");
string input = Console.ReadLine();
Console.WriteLine(ReverseString(input));

//2
public class SyracuseHypothesis
{
    public static void Main()
    {
        Console.Write("write first number: ");
        int number = int.Parse(Console.ReadLine());

        Console.WriteLine("Syracuse sequence:");
        PrintSyracuseSequence(number);

        Console.ReadLine();
    }

    public static void PrintSyracuseSequence(int number)
    {
        Console.WriteLine(number);

        if (number == 1)
            return;

        if (number % 2 == 0)
            PrintSyracuseSequence(number / 2);
        else
            PrintSyracuseSequence(number * 3 + 1);
    }
}

//3
public class WordFilter
{
    public static void Main()
    {
        string sentence = "This is a stroke with any not allowed words.";

        string[] allowedWords = { "stroke", "with", "any", "words" };

        string filteredSentence = FilterWords(sentence, allowedWords);

        Console.WriteLine("Originat stroke: " + sentence);
        Console.WriteLine("Filtered stroke: " + filteredSentence);

        Console.ReadLine();
    }

    public static string FilterWords(string sentence, string[] allowedWords)
    {
        string[] words = sentence.Split(' ');

        var filteredWords = words.Where(word => allowedWords.Contains(word));

        string filteredSentence = string.Join(" ", filteredWords);

        return filteredSentence;
    }
}

//4
public class RandomStringGenerator
{
    public static void Main()
    {
        int length = 10;
        string randomString = GenerateRandomString(length);

        Console.WriteLine("Random stroke: " + randomString);

        Console.ReadLine();
    }

    public static string GenerateRandomString(int length)
    {
        Random random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        StringBuilder stringBuilder = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            int randomIndex = random.Next(chars.Length);
            char randomChar = chars[randomIndex];
            stringBuilder.Append(randomChar);
        }

        return stringBuilder.ToString();
    }
}

//5
public class MissingNumber
{
    public static void Main()
    {
        int[] array = { 2, 3, 1, 0, 5, 4, 7, 6, 9 }; // Приклад масиву з пропущеним числом

        int missingNumber = FindMissingNumber(array);

        Console.WriteLine("Missing number: " + missingNumber);

        Console.ReadLine();
    }

    public static int FindMissingNumber(int[] array)
    {
        int missingNumber = array.Length;

        for (int i = 0; i < array.Length; i++)
        {
            missingNumber ^= i ^ array[i];
        }

        return missingNumber;
    }
}

//6
public class GameOfLife
{
    private static int Width = 10;
    private static int Height = 10;
    private static bool[,] cells = new bool[Width, Height];

    public static void Main()
    {
        SetInitialConfiguration();

        StartGame();
    }

    public static void SetInitialConfiguration()
    {
        cells[1, 2] = true;
        cells[2, 3] = true;
        cells[3, 1] = true;
        cells[3, 2] = true;
        cells[3, 3] = true;
    }

    public static void StartGame()
    {
        while (true)
        {
            Console.Clear();
            PrintCells();

            UpdateCells();
            Thread.Sleep(500);
        }
    }

    public static void PrintCells()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (cells[x, y])
                {
                    Console.Write("■");
                }
                else
                {
                    Console.Write("□");
                }
            }
            Console.WriteLine();
        }
    }

    public static void UpdateCells()
    {
        bool[,] newCells = new bool[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                int liveNeighbors = CountLiveNeighbors(x, y);

                if (cells[x, y])
                {
                    if (liveNeighbors == 2 || liveNeighbors == 3)
                    {
                        newCells[x, y] = true;
                    }
                    else
                    {
                        newCells[x, y] = false;
                    }
                }
                else
                {
                    if (liveNeighbors == 3)
                    {
                        newCells[x, y] = true;
                    }
                    else
                    {
                        newCells[x, y] = false;
                    }
                }
            }
        }

        cells = newCells;
    }

    public static int CountLiveNeighbors(int x, int y)
    {
        int liveNeighbors = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                int neighborX = x + i;
                int neighborY = y + j;

                if (neighborX >= 0 && neighborX < Width && neighborY >= 0 && neighborY < Height)
                {
                    if (cells[neighborX, neighborY])
                    {
                        liveNeighbors++;
                    }
                }
            }
        }

        return liveNeighbors;
    }
}


//7
public class DNACompression
{
    public static void Main()
    {
        string dnaSequence = "ACGTACGTACGT";

        string compressedSequence = CompressDNA(dnaSequence);
        Console.WriteLine("Compressed sequence DNA: " + compressedSequence);

        string decompressedSequence = DecompressDNA(compressedSequence);
        Console.WriteLine("Decompressed sequence DNA: " + decompressedSequence);
    }

    public static string CompressDNA(string sequence)
    {
        StringBuilder compressedSequence = new StringBuilder();

        int count = 1;
        char currentChar = sequence[0];

        for (int i = 1; i < sequence.Length; i++)
        {
            if (sequence[i] == currentChar)
            {
                count++;
            }
            else
            {
                compressedSequence.Append(currentChar);
                compressedSequence.Append(count);

                currentChar = sequence[i];
                count = 1;
            }
        }

        compressedSequence.Append(currentChar);
        compressedSequence.Append(count);

        return compressedSequence.ToString();
    }

    public static string DecompressDNA(string compressedSequence)
    {
        StringBuilder decompressedSequence = new StringBuilder();

        for (int i = 0; i < compressedSequence.Length; i += 2)
        {
            char nucleotide = compressedSequence[i];
            int count = int.Parse(compressedSequence[i + 1].ToString());

            for (int j = 0; j < count; j++)
            {
                decompressedSequence.Append(nucleotide);
            }
        }

        return decompressedSequence.ToString();
    }
}


//8
public class SymmetricEncryption
{
    private static byte[] Key;
    private static byte[] IV;

    public static void Main()
    {
        string plainText = "secret info";

        GenerateKeyAndIV();

        byte[] encryptedBytes = Encrypt(plainText);
        string encryptedText = Convert.ToBase64String(encryptedBytes);
        Console.WriteLine("Encrypted text: " + encryptedText);

        string decryptedText = Decrypt(encryptedBytes);
        Console.WriteLine("Decrypted text: " + decryptedText);
    }

    public static void GenerateKeyAndIV()
    {
        using (Aes aes = Aes.Create())
        {
            Key = aes.Key;
            IV = aes.IV;
        }
    }

    public static byte[] Encrypt(string plainText)
    {
        byte[] encryptedBytes;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }

                encryptedBytes = ms.ToArray();
            }
        }

        return encryptedBytes;
    }

    public static string Decrypt(byte[] encryptedBytes)
    {
        string decryptedText;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        decryptedText = reader.ReadToEnd();
                    }
                }
            }
        }

        return decryptedText;
    }
}