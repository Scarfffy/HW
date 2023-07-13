using System;
using System.Collections.Generic;

class fairy_tale
{
    static void Main(string[] args)
    {
        List<Character> characters = new List<Character>();
        characters.Add(new Character("Grandfather"));
        characters.Add(new Character("Grandmother"));
        characters.Add(new Character("Granddaughter"));
        characters.Add(new Character("Zhuchka"));

        Turnip turnip = new Turnip(10);

        Console.WriteLine("There lived Grandfather, Grandmother, Granddaughter, Zhuchka and Ripka.");
        Console.WriteLine("Grandfather planted a turnip and it began to grow.");

        bool isPulled = false;
        int attempts = 0;

        while (!isPulled && attempts < characters.Count)
        {
            Character currentCharacter = characters[attempts];

            if (currentCharacter.Pull(turnip.Weight))
            {
                Console.WriteLine($"{currentCharacter.Name} pulled out the turnip!");
                isPulled = true;
            }
            else
            {
                Console.WriteLine($"{currentCharacter.Name} could not pull out the turnip.");
                attempts++;
            }
        }

        if (!isPulled)
        {
            Console.WriteLine("No one could pull out the turnip, it became too big!");
        }
    }
}

class Character
{
    public string Name { get; }

    public Character(string name)
    {
        Name = name;
    }

    public bool Pull(int turnipWeight)
    {
        Random random = new Random();
        int strength = random.Next(1, 10);

        if (strength >= turnipWeight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

class Turnip
{
    public int Weight { get; set; }

    public Turnip(int weight)
    {
        Weight = weight;
    }
}
