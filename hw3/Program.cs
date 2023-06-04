using System;

class Program
{
    public static string P1;
    public static string cpu = "Bot";
    public static int p1Score;
    public static int cpuScore;
    public static int p1Wins;
    public static int cpuWins;
    public static int round = 1;
    public static bool won = false;
    static string[] suits = { "Spades", "Clubs", "Hearts", "Diamonds" };
    static string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
    static Random random = new Random();

    static void Main(string[] args)
    {
        Console.WriteLine("\nWELCOME TO THE GAME 21\n");
        Console.WriteLine("What is your name?");
        P1 = Console.ReadLine();
        StartGame();
    }

    public static void StartGame()
    {
        Console.WriteLine($"\nWelcome {P1}! Let's play!");
        Console.WriteLine("Objective: Reach the highest score without going over 21\n");

        while (true)
        {
            if (round == 1)
            {
                int p1Card = GetRandomCard();
                int cpuCard = GetRandomCard();

                p1Score += p1Card;
                cpuScore += cpuCard;

                Console.WriteLine($"{P1}, you are dealt a {GetCardString(p1Card)}");
                Console.WriteLine($"{cpu} is dealt a {GetCardString(cpuCard)}");

                round++;
            }
            else if (round > 1 && !won)
            {
                if (cpuScore > p1Score)
                {
                    Console.Write($"{P1}, please select another card. (Y): ");
                }
                else
                {
                    Console.Write("Would you like another card? (Y/N): ");
                }

                string message = Console.ReadLine();

                if (message.ToLower() == "y" && !won)
                {
                    int p1Card = GetRandomCard();
                    p1Score += p1Card;

                    Console.WriteLine($"{P1}, you are dealt a {GetCardString(p1Card)} Total value: {p1Score}");

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
                        int cpuCard = GetRandomCard();
                        cpuScore += cpuCard;

                        Console.WriteLine($"{cpu} is dealt a {GetCardString(cpuCard)} Total value: {cpuScore}");

                        CheckBust(p1Score, cpuScore);

                        if (cpuScore > p1Score)
                        {
                            Console.WriteLine($"{cpu} wins! You lose! :(");
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
        Console.Write("Continue playing? (Y/N): ");
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

        Console.WriteLine($"\n{P1}: {p1Wins}  | {cpu}: {cpuWins}\n");
    }

    static void CheckBust(int score, int cpuscore)
    {
        if (score > 21)
        {
            Console.WriteLine($"{P1}, you Bust! Dealer wins.");
            cpuWins++;
            ContinuePlaying();
        }
        if (cpuscore > 21)
        {
            Console.WriteLine($"{cpu} Bust! {P1} wins.");
            p1Wins++;
            ContinuePlaying();
        }
    }

    static int GetRandomCard()
    {
        int randomCards = random.Next(0, 12);
        if (randomCards == 9)
        {
            return 2;
        }
        else if (randomCards == 10)
        {
            return 3;
        }
        else if (randomCards == 11)
        {
            return 4;
        }
        else if (randomCards == 12)
        {
            return 11;
        }
        else
        {
            return randomCards;
        }
    }

    static string GetCardString(int cardValue)
    {
        string rank = ranks[cardValue - 2];
        string suit = suits[random.Next(0, suits.Length)];

        int score = GetCardScore(rank);

        return $"{rank} of {suit} ({score} points)";
    }

    static int GetCardScore(string rank)
    {
        switch (rank)
        {
            case "Ace":
                return 11;
            case "King":
                return 4;
            case "Queen":
                return 3;
            case "Jack":
                return 2;
            default:
                return int.Parse(rank);
        }
    }
}