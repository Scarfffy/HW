using System;
using System.Collections.Generic;
using System.Linq;

class Film
{
    public string Name { get; set; }
    public string Director { get; set; }
}

class Director
{
    public string Name { get; set; }
    public string Country { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        List<Film> films = new List<Film>()
        {
            new Film { Name = "The Silence of the Lambs", Director ="Jonathan Demme" },
            new Film { Name = "The World's Fastest Indian", Director ="Roger Donaldson" },
            new Film { Name = "The Recruit", Director ="Roger Donaldson" }
        };

        List<Director> directors = new List<Director>()
        {
            new Director {Name="Jonathan Demme", Country="USA"},
            new Director {Name="Roger Donaldson", Country="New Zealand"},
        };

        Console.WriteLine(films.Select(film => film.Name).Aggregate((result, filmName) => result + ", " + filmName));

        Console.WriteLine(films.Aggregate("", (result, film) => result + film.Name));

        Console.WriteLine(string.Join(", ", films.Select(film => $"{film.Name} {film.Director} ({directors.FirstOrDefault(director => director.Name == film.Director)?.Country})")));

        Console.WriteLine(string.Join(", ", directors.Select(director => $"{director.Name}: {string.Join(", ", films.Where(film => film.Director == director.Name).Select(film => film.Name))}")));

        Console.ReadLine();
    }
}