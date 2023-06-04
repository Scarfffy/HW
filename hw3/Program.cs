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
    //static string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
    static Random random = new Random();

    static void Main(string[] args)
    {
        Console.WriteLine("\nWELCOME TO THE GAME 21\n");
        Thread.Sleep(1000);
        Console.WriteLine("What is your name?");
        P1 = Console.ReadLine();
        Thread.Sleep(500);
        StartGame();
    }

    public static void StartGame()
    {
        Console.WriteLine($"\nWelcome {P1}! Let's play!");
        Console.WriteLine("Objective: Reach the highest score without going over 21, Jack - 2 points, Queen - 3 points, King - 4 points, Ace - 11 points\n");
        Thread.Sleep(1000);

        while (true)
        {
            if (round == 1)
            {
                int p1Card = GetRandomCard();
                int cpuCard = GetRandomCard();

                if (p1Card == 9) //player cards
                {
                    p1Score += 2;
                }
                else if (p1Card == 10)
                {
                    p1Score += 3;
                }
                else if (p1Card == 11)
                {
                    p1Score += 4;
                }
                else if (p1Card == 12)
                {

                    p1Score += 11;
                }
                else
                {
                    p1Score += p1Card;
                }

                if (cpuCard == 9) //bot cards
                {
                    cpuScore += 2;
                }
                else if (cpuCard == 10)
                {
                    cpuScore += 3;
                }
                else if (cpuCard == 11)
                {
                    cpuScore += 4;
                }
                else if (cpuCard == 12)
                {
                    cpuScore += 11;
                }
                else
                {
                    cpuScore += cpuCard;
                }

                Console.WriteLine($"{P1}, you are dealt a {GetCardString(p1Card)}");
                Thread.Sleep(1000);
                Console.WriteLine($"{cpu} is dealt a {GetCardString(cpuCard)}");
                Thread.Sleep(1000);

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

                    if (p1Card == 9)
                    {
                        p1Score += 2;
                    }
                    else if (p1Card == 10)
                    {
                        p1Score += 3;
                    }
                    else if (p1Card == 11)
                    {
                        p1Score += 4;
                    }
                    else if (p1Card == 12)
                    {

                        p1Score += 11;
                    }
                    else
                    {
                        p1Score += p1Card;
                    }

                    Console.WriteLine($"{P1}, you are dealt a {GetCardString(p1Card)} Total value: {p1Score}");
                    Thread.Sleep(1000);

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

                        if (cpuCard == 9)
                        {
                            cpuScore += 2;
                        }
                        else if (cpuCard == 10)
                        {
                            cpuScore += 3;
                        }
                        else if (cpuCard == 11)
                        {
                            cpuScore += 4;
                        }
                        else if (cpuCard == 12)
                        {
                            cpuScore += 11;
                        }
                        else
                        {
                            cpuScore += cpuCard;
                        }

                        Console.WriteLine($"{cpu} is dealt a {GetCardString(cpuCard)} Total value: {cpuScore}");

                        CheckBust(p1Score, cpuScore);
                        Thread.Sleep(1000);

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
        switch (randomCards)
        {
            case 0:
                return 2;
            case 1:
                return 3;
            case 2:
                return 4;
            case 3:
                return 5;
            case 4:
                return 6;
            case 5:
                return 7;
            case 6:
                return 8;
            case 7:
                return 9;
            case 8:
                return 10;
        }
        return randomCards;
    }

    static string GetCardString(int cardValue)
    {
        string suit = suits[random.Next(0, suits.Length)];
        int rank = cardValue;
        switch (cardValue)
        {
            case 9:
                return $"Jack of {suit}" ;
            case 10:
                return $"Queen of {suit}";
            case 11:
                return $"King of {suit}";
            case 12:
                return $"Ace of {suit}";
        }
        return $"{rank} of {suit}";
    }
}