using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using System.Configuration;

namespace SimpleMovieClassDemo.Models
{
    internal class SimpleMoviesApp
    {
        string FilePath = ConfigurationManager.AppSettings["MyFile"];
        Movie[] movies;

        const int MaxMovies = 5;
        int Count;

        public SimpleMoviesApp()
        {
            movies = new Movie[MaxMovies];
            Count = LoadMovies();
        }

        public int LoadMovies()
        {
            if (File.Exists(FilePath))
            {
                Console.WriteLine("Data Successfully Deserialized");
                string json = File.ReadAllText(FilePath);
                Movie[] loadedMovies = JsonConvert.DeserializeObject<Movie[]>(json);
                if (loadedMovies != null)
                {
                    Array.Copy(loadedMovies, movies, loadedMovies.Length);
                    return loadedMovies.Length;
                }
                
            }
            return 0; 
        }

        public void SaveMovies()
        {
            var nonNullMovies = movies.Where(movie => movie != null).ToArray();
            string json = JsonConvert.SerializeObject(nonNullMovies);
            File.WriteAllText(FilePath, json);
        }

        public void DisplayMovies()
        {
            if (Count == 0)
            {
                Console.WriteLine("No movies available");
                return;
            }

            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine(movies[i]);
            }
        }

        public void AddMovie()
        {
            if (Count >= MaxMovies)
            {
                Console.WriteLine("Movie list is full.Please clear some movies");
                return;
            }

            Movie newMovie = new Movie
            {
                MovieId = Count + 1
            };

            //For User-Defined Movie ID
            //Console.WriteLine("Enter Movie ID");
            //newMovie.MovieId = int.Parse(Console.ReadLine());

            Console.Write("Enter movie name: ");
            newMovie.Name = Console.ReadLine();

            Console.Write("Enter movie genre: ");
            newMovie.Genre = Console.ReadLine();

            Console.Write("Enter release year: ");
            string input = Console.ReadLine();

            int releaseYear;

            while (!int.TryParse(input, out releaseYear))
            {
                Console.Write("Invalid year.Please enter a valid release year: ");
                input = Console.ReadLine();
            }

            newMovie.Year = releaseYear;

            movies[Count] = newMovie;
            Count++;
            SaveMovies();
            Console.WriteLine("Movie added successfully.");
        }

        public void ClearAll()
        {
            for (int i = 0; i < movies.Length; i++)
            {
                movies[i] = null;
            }
            Count = 0;
            SaveMovies();
            Console.WriteLine("All movies cleared");
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Display Movies");
                Console.WriteLine("2. Add Movie");
                Console.WriteLine("3. Clear All Movies");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayMovies();
                        break;
                    case "2":
                        AddMovie();
                        break;
                    case "3":
                        ClearAll();
                        break;
                    case "4":
                        Console.WriteLine("Exiting");
                        return;
                    default:
                        Console.WriteLine("Invalid choice.Please try again.");
                        break;
                }
            }
        }
    }
}
