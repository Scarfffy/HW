var data = new List<object>() {
  "Hello",
  new Book() { Author = "Terry Pratchett", Name = "Guards! Guards!", Pages = 810 },
  new List<int>() {4, 6, 8, 2},
  new string[] {"Hello inside array"},
  new Film() { Author = "Martin Scorsese", Name= "The Departed", Actors = new List<Actor>() {
    new Actor() { Name = "Jack Nickolson", Birthdate = new DateTime(1937, 4, 22)},
    new Actor() { Name = "Leonardo DiCaprio", Birthdate = new DateTime(1974, 11, 11)},
    new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)}
  }},
  new Film() { Author = "Gus Van Sant", Name = "Good Will Hunting", Actors = new List<Actor>() {
    new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)},
    new Actor() { Name = "Robin Williams", Birthdate = new DateTime(1951, 8, 11)},
}},
  new Book() { Author = "Stephen King", Name="Finders Keepers", Pages = 200},
  "Leonardo DiCaprio"
  };




// 1. Виведіть усі елементи, крім ArtObjects
Console.WriteLine(string.Join(", ", data.Where(item => !(item is ArtObject))));
Console.WriteLine();

// 2. Виведіть імена всіх акторів
Console.WriteLine(string.Join(", ", data.OfType<Film>().SelectMany(film => film.Actors).Select(actor => actor.Name)));
Console.WriteLine();

// 3. Виведіть кількість акторів, які народилися в серпні
Console.WriteLine(data.OfType<Film>().SelectMany(film => film.Actors).Count(actor => actor.Birthdate.Month == 8));
Console.WriteLine();

// 4. Виведіть два найстаріших імена акторів
Console.WriteLine(string.Join(", ", data.OfType<Film>().SelectMany(film => film.Actors).OrderByDescending(actor => actor.Birthdate).Take(2).Select(actor => actor.Name)));
Console.WriteLine();

// 5. Вивести кількість книг на авторів
Console.WriteLine(string.Join(", ", data.OfType<Book>().GroupBy(book => book.Author).Select(group => $"{group.Key}: {group.Count()}")));
Console.WriteLine();

// 6. Виведіть кількість книг на одного автора та фільмів на одного режисера
Console.WriteLine($"Number of books per author: {data.OfType<Book>().Count() / (double)data.OfType<Book>().Select(book => book.Author).Distinct().Count()}");
Console.WriteLine($"Number of films per director: {data.OfType<Film>().Count() / (double)data.OfType<Film>().Select(film => film.Author).Distinct().Count()}");
Console.WriteLine();

// 7. Виведіть, скільки різних букв використано в іменах усіх акторів
Console.WriteLine(data.OfType<Film>().SelectMany(film => film.Actors).SelectMany(actor => actor.Name).Distinct().Count());
Console.WriteLine();

// 8. Виведіть назви всіх книг, упорядковані за іменами авторів і кількістю сторінок
Console.WriteLine(string.Join(", ", data.OfType<Book>().OrderBy(book => book.Author).ThenBy(book => book.Pages).Select(book => $"{book.Name} ({book.Author}, {book.Pages} pages)")));
Console.WriteLine();

// 9. Виведіть ім'я актора та всі фільми за участю цього актора
Console.WriteLine(string.Join(", ", data.OfType<Film>().SelectMany(film => film.Actors, (film, actor) => new { ActorName = actor.Name, FilmName = film.Name }).GroupBy(entry => entry.ActorName).Select(group => $"{group.Key}: {string.Join(", ", group.Select(entry => entry.FilmName))}")));
Console.WriteLine();

// 10. Виведіть суму загальної кількості сторінок у всіх книгах і всі значення int у всіх послідовностях у даних
Console.WriteLine($"The sum of the total number of pages in all books: {data.OfType<Book>().Sum(book => book.Pages)}");
Console.WriteLine($"All int values ​​in sequences in data: {string.Join(", ", data.OfType<IEnumerable<int>>().SelectMany(seq => seq))}");
Console.WriteLine();

// 11. Отримати словник з ключем - автор книги, значенням - список авторських книг
Console.WriteLine(string.Join(", ", data.OfType<Book>().GroupBy(book => book.Author).ToDictionary(group => group.Key, group => string.Join(", ", group.Select(book => book.Name)))));
Console.WriteLine();

// 12. Вивести всі фільми "Метт Деймон", за винятком фільмів з акторами, імена яких представлені в даних у вигляді рядків
Console.WriteLine(string.Join(", ", data.OfType<Film>().Where(film => film.Author == "Matt Damon" && !film.Actors.Any(actor => data.OfType<string>().Contains(actor.Name))).Select(film => film.Name)));
Console.WriteLine();






class Actor
{
    public string Name { get; set; }
    public DateTime Birthdate { get; set; }
}

abstract class ArtObject
{
    public string Author { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}

class Film : ArtObject
{
    public int Length { get; set; }
    public IEnumerable<Actor> Actors { get; set; }
}

class Book : ArtObject
{
    public int Pages { get; set; }
}