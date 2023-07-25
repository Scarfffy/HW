using System;
using System.IO;

class Program
{
    static void Main()
    {
        string currentPath = Directory.GetCurrentDirectory();
        DisplayDirectoryContents(currentPath);

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                if (IsDirectorySelected(currentPath))
                {
                    currentPath = Path.Combine(currentPath, GetSelectedEntryName());
                    DisplayDirectoryContents(currentPath);
                }
                else if (IsTextFileSelected(currentPath))
                {
                    try
                    {
                        string filePath = Path.Combine(currentPath, GetSelectedEntryName());
                        string fileContent = File.ReadAllText(filePath);
                        Console.Clear();
                        Console.WriteLine(fileContent);
                        Console.WriteLine("\nPress any key to go back...");
                        Console.ReadKey();
                        DisplayDirectoryContents(currentPath);
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine($"Error reading file: {ex.Message}");
                        Console.WriteLine("\nPress any key to go back...");
                        Console.ReadKey();
                        DisplayDirectoryContents(currentPath);
                    }
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                if (currentPath != Directory.GetDirectoryRoot(currentPath))
                {
                    currentPath = Directory.GetParent(currentPath).FullName;
                    DisplayDirectoryContents(currentPath);
                }
            }
            else if (key.Key == ConsoleKey.C)
            {
                if (IsDirectorySelected(currentPath))
                {
                    string sourcePath = Path.Combine(currentPath, GetSelectedEntryName());
                    string destinationPath = GetDestinationPath();
                    CopyDirectory(sourcePath, destinationPath);
                    DisplayDirectoryContents(currentPath);
                }
                else if (IsTextFileSelected(currentPath))
                {
                    Console.WriteLine("Copying text files is not supported.");
                }
            }
            else if (key.Key == ConsoleKey.M)
            {
                if (IsDirectorySelected(currentPath))
                {
                    string sourcePath = Path.Combine(currentPath, GetSelectedEntryName());
                    string destinationPath = GetDestinationPath();
                    MoveDirectory(sourcePath, destinationPath);
                    DisplayDirectoryContents(currentPath);
                }
                else if (IsTextFileSelected(currentPath))
                {
                    Console.WriteLine("Moving text files is not supported.");
                }
            }
            else if (key.Key == ConsoleKey.D)
            {
                if (IsDirectorySelected(currentPath))
                {
                    string directoryPath = Path.Combine(currentPath, GetSelectedEntryName());
                    DeleteDirectory(directoryPath);
                    DisplayDirectoryContents(currentPath);
                }
                else if (IsTextFileSelected(currentPath))
                {
                    string filePath = Path.Combine(currentPath, GetSelectedEntryName());
                    DeleteFile(filePath);
                    DisplayDirectoryContents(currentPath);
                }
            }
        }
    }

    static void DisplayDirectoryContents(string path)
    {
        Console.Clear();
        Console.WriteLine($"Current Directory: {path}\n");

        string[] directories = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);

        Console.WriteLine("Directories:");
        foreach (string directory in directories)
        {
            Console.WriteLine($"[DIR] {Path.GetFileName(directory)}");
        }

        Console.WriteLine("\nFiles:");
        foreach (string file in files)
        {
            Console.WriteLine($"[FILE] {Path.GetFileName(file)}");
        }

        Console.WriteLine("\nPress Enter to open, Escape to go back");
        Console.WriteLine("Press C to copy, M to move, D to delete\n");
    }

    static bool IsDirectorySelected(string path)
    {
        string selectedEntry = GetSelectedEntryName();
        string selectedEntryPath = Path.Combine(path, selectedEntry);
        return Directory.Exists(selectedEntryPath);
    }

    static bool IsTextFileSelected(string path)
    {
        string selectedEntry = GetSelectedEntryName();
        string selectedEntryPath = Path.Combine(path, selectedEntry);
        return File.Exists(selectedEntryPath) && Path.GetExtension(selectedEntryPath).Equals(".txt", StringComparison.OrdinalIgnoreCase);
    }

    static string GetSelectedEntryName()
    {
        string[] lines = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return lines.Length > 0 ? lines[0] : string.Empty;
    }

    static string GetDestinationPath()
    {
        Console.WriteLine("Enter the destination path:");
        return Console.ReadLine();
    }

    static void CopyDirectory(string sourcePath, string destinationPath)
    {
        try
        {
            Directory.CreateDirectory(destinationPath);

            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

            foreach (string filePath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(filePath, filePath.Replace(sourcePath, destinationPath), true);

            Console.WriteLine("Directory copied successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying directory: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static void MoveDirectory(string sourcePath, string destinationPath)
    {
        try
        {
            Directory.Move(sourcePath, destinationPath);
            Console.WriteLine("Directory moved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error moving directory: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static void DeleteDirectory(string directoryPath)
    {
        try
        {
            Directory.Delete(directoryPath, true);
            Console.WriteLine("Directory deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting directory: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static void DeleteFile(string filePath)
    {
        try
        {
            File.Delete(filePath);
            Console.WriteLine("File deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
