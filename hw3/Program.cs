using System;
using System.Threading;

class Program
{

    public static string P1;
    public static string cpu = "Bot";
    public static int p1Score = 0;
    public static int cpuScore = 0;
    public static int p1Wins = 0;
    public static int cpuWins = 0;
    public static int round = 1;
    public static bool won = false;

    static void Main(string[] args)
    {
        Console.WriteLine ("\nWELCOME TO THE GAME 21\n");
        Console.WriteLine("What is your name?");

        P1 = Console.ReadLine();

        StartGame();
    }

    public static void StartGame()
    {

        Console.WriteLine($"\nWelcome {P1} lets play!");
        Console.WriteLine($"Objective: Stand with the highest count without going over 21\n");

        while (true)
        {
            Random random = new Random();

            if (round == 1)
            {
                int p1Card = random.Next(1, 11);
                int cpuCard = random.Next(1, 11);

                p1Score += p1Card;
                cpuScore += cpuCard;

                Console.WriteLine($"{P1} you are dealt a {p1Card}\n");
                Console.WriteLine($"{cpu} is dealt a {cpuCard}\n");

                round++;
            }
            if (round > 1 && !won)
            {
                if (cpuScore > p1Score)
                {
                    Console.Write($"{P1} please select another card. (Y)");
                }
                else
                    Console.Write("Would you like another card? (Y/N)");
                string message = Console.ReadLine();

                if (message.ToLower() == "y" && !won)
                {

                    int p1Card = random.Next(1, 11);

                    p1Score += p1Card;

                    Console.WriteLine($"{P1} you are dealt a {p1Card} total value: {p1Score}\n");

                    CheckBust(p1Score, cpuScore);

                    if (won)
                    {
                        return;
                    }

                }
                else if (round > 1 && message.ToLower() == "n" && !won)
                {
                    while (cpuScore <= p1Score && p1Score > 0)
                    {
                        int cpuCard = random.Next(1, 11);

                        cpuScore += cpuCard;

                        Console.WriteLine($"{cpu} is dealt a {cpuCard} total value {cpuScore}\n");

                        CheckBust(p1Score, cpuScore);

                        if (cpuScore > p1Score)
                        {
                            Console.WriteLine($"{cpu} wins! you lose!  :(");
                            cpuWins++;

                            ContinuePlaying();
                        }

                    }


                }

            }


        }

    }
    static void ContinuePlaying()
    {

        Console.Write("Continue playing? (Y/N)");
        string response = Console.ReadLine();

        if (response.ToLower() == "y")
        {
            ResetGame();
        }
        else
        {
            ExitGame();

        }

    }

    private static void ExitGame()
    {
        Environment.Exit(0);
    }

    private static void ResetGame()
    {
        p1Score = 0;
        cpuScore = 0;
        round = 1;

        Console.Clear();

        string rnd = "Round ";
        int rndNum = cpuWins + p1Wins + 1;

        Console.WriteLine($"\n{P1} : {p1Wins}  | {cpu} : {cpuWins}\n");
    }

    static void CheckBust(int score, int cpuscore)
    {
        if (score > 21)
        {
            Console.WriteLine($"{P1} you Bust, dealer wins");

            cpuWins++;

            ContinuePlaying();
        }
        if (cpuscore > 21)
        {
            Console.WriteLine($"{cpu} Bust, {P1} wins");

            p1Wins++;

            ContinuePlaying();

        }


    }
}