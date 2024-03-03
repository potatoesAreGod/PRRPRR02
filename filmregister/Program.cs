class Movie(string name, string director, int length)
{
    public string Name { get; set; } = name;
    public string Director { get; set; } = director;
    public int Length { get; set; } = length;

    public override string ToString()
    {
        // Convert minutes to hours and minutes
        int hours = (Length - Length % 60) / 60;
        int minutes = (Length - hours * 60);

        // Return movie info
        if (hours == 0)
            return $"Name: {Name}, Director: {Director}, Length: {minutes} minutes";

        return $"Name: {Name}, Director: {Director}, Length: {hours} hours and {minutes} minutes";
    }
}

class MovieRegistry
{
    private readonly List<Movie> movies;

    public MovieRegistry()
    {
        movies = [];
    }

    public void AddMovie()
    {
        // Ask for the movie name
        Console.Write("Enter movie name: ");
        string name = Console.ReadLine();

        // Make sure the name is a real name
        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Please enter a valid movie name");
            Console.Write("Enter movie name: ");
            name = Console.ReadLine();
        }

        // Ask for director
        Console.Write("Enter director: ");
        string director = Console.ReadLine();

        // Make sure the user entered something
        while (string.IsNullOrWhiteSpace(director))
        {
            Console.WriteLine("Please enter a valid director");
            Console.Write("Enter director: ");
            director = Console.ReadLine();
        }

        // Ask for movie length
        Console.Write("Enter length (in minutes): ");
        int length;

        // Make sure the user enters a positive whole number
        while (!int.TryParse(Console.ReadLine(), out length) || length <= 0)
        {
            Console.WriteLine("Please enter a valid positive number for length");
            Console.Write("Enter length (in minutes): ");
        }

        // Add movie to the list
        Movie newMovie = new(name, director, length);
        movies.Add(newMovie);

        // Write a confirmation to the user
        Console.WriteLine("{0} was added successfully!", name);
    }

    // Function for displaying all entered movies
    public void DisplayMovies()
    {
        // If there's no movies, print a special message
        if (movies.Count == 0)
        {
            Console.WriteLine("No movies in the registry");
        }
        // Write each movie on a new line
        else
        {
            foreach (var movie in movies)
                Console.WriteLine(movie);
        }
    }

    public void RemoveMovie()
    {
        if (movies.Count == 0)
        {
            Console.WriteLine("No movies in the registry to remove");
            return;
        }

        // Ask for the movie name
        Console.Write("Enter the movie title you want to remove: ");
        string name = Console.ReadLine();

        // Make sure the name is a real name
        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Please enter a valid movie name");
            Console.Write("Enter movie to remove: ");
            name = Console.ReadLine();
        }

        // Enumerate through all movies to find the title to remove
        foreach (var movie in movies.ToList())
        {
            if (movie.Name == name)
            { 
                movies.Remove(movie);
                Console.WriteLine("{0} was removed", movie.Name);
                return;
            }
        }
        Console.WriteLine("{0} was not found. No movies removed", name);
    }
}

class Program
{
    static void Main()
    {
        // Create a new instance to save movies in
        MovieRegistry movieRegistry = new();

        // Asks the user what they want to do
        while (true)
        {
            Console.WriteLine("1. Add new movie");
            Console.WriteLine("2. Display all movies");
            Console.WriteLine("3. Remove a movie");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice (1-4): ");
            string res = Console.ReadLine() ?? "";

            switch (res)
            {
                case "1":
                    movieRegistry.AddMovie();
                    break;

                case "2":
                    movieRegistry.DisplayMovies();
                    break;

                case "3":
                    movieRegistry.RemoveMovie();
                    break;

                case "4":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Enter a number between 1 and 4");
                    break;
            }
        }
    }
}
