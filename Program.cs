using System;
using MovieList.Model;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MovieApp
{
    class Program
    {
        const int MAX_MOVIES = 5;
        static string filePath = ConfigurationManager.AppSettings["moviefile"];
        static List<Movie> movies = new List<Movie>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Display Movies");
                Console.WriteLine("2. Add Movie");
                Console.WriteLine("3. Find Movie by Year");
                Console.WriteLine("4. Remove Movie by Name");
                Console.WriteLine("5. Clear List");
                Console.WriteLine("6. Exit");

                Console.Write("Choice: ");
                int choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DisplayMovies();
                        break;

                    case 2:
                        AddMovie();
                        break;

                    case 3:
                        FindByYear();
                        break;

                    case 4:
                        RemoveByName();
                        break;

                    case 5:
                        movies.Clear();
                        Console.WriteLine("Movies cleared");
                        SaveMovies();

                        break;

                    case 6:
                        SaveMovies();
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void DisplayMovies()
        {
            if (movies.Count == 0)
            {
                Console.WriteLine("No movies in list!");
                return;
            }

            foreach (Movie movie in movies)
            {
                Console.WriteLine(movie.Id + "-" +movie.Name + " (" + movie.Year + ") - " + movie.Director);
            }
        }

        static void AddMovie()
        {
            if (movies.Count >= MAX_MOVIES)
            {
                Console.WriteLine("List is full. Maximum " + MAX_MOVIES + " movies.");
                return;
            }

            Console.WriteLine("Enter movie id:");
            int id = Int32.Parse(Console.ReadLine());

            Console.Write("Enter movie name: ");
            string name = Console.ReadLine();

            Console.Write("Enter movie year: ");
            int year = Int32.Parse(Console.ReadLine());

            Console.Write("Enter director: ");
            string director = Console.ReadLine();

            Movie movie = new Movie() { Id = id, Name = name, Year = year, Director = director };

            movies.Add(movie);
            Console.WriteLine("Movie added successfully!");
            SaveMovies();
        }

        static void FindByYear()
        {
            Console.Write("Enter year to search for: ");
            int yearToSearch = Int32.Parse(Console.ReadLine());

            List<Movie> moviesFound = movies.FindAll(movie => movie.Year == yearToSearch);

            if (moviesFound.Count == 0)
            {
                Console.WriteLine($"No movies found for year {yearToSearch}.");
            }
            else
            {
                Console.WriteLine($"Movies found for year {yearToSearch}:");
                foreach (Movie movie in moviesFound)
                {
                    Console.WriteLine($"{movie.Name} ({movie.Year}) - {movie.Director}");
                }
            }
        }
        static void RemoveByName()
        {
            Console.WriteLine("Enter the name to search for:");
            string movieNameToRemove = Console.ReadLine();
            Movie movieToRemove = movies.Find(movie => movie.Name == movieNameToRemove);

            if (movieToRemove == null)
            {
                Console.WriteLine($"No movie found with the name '{movieNameToRemove}'.");
            }
            else
            {
                movies.Remove(movieToRemove);
                Console.WriteLine($"Movie '{movieNameToRemove}' removed successfully!");
                SaveMovies();
            }
        }

        static void SaveMovies()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Movie movie in movies)
                {
                    writer.WriteLine(movie.Id + "," +movie.Name + "," + movie.Year + "," + movie.Director);
                }
            }
        }

        
    }
}