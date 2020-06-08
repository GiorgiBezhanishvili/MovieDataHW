using MovieData.Enums;
using MovieData.Models;
using MovieData.Repositories;
using MovieData.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;

namespace MovieData
{
    class Program
    {
        static void Main(string[] args)
        {
			
			MovieRepository movieRepository = new MovieRepository();

			Start:
			// Print All Movies
			Console.WriteLine("All Movies");
			var movies = movieRepository.GetAll();
			Print(movies);

			Retry:
			// Options
			Console.WriteLine("Choose the Operation : Delete,Create,Update or GetById; Write Down your Option");
			Console.WriteLine("~~~If you want to EXIT write down~~~");
			var option = Console.ReadLine().ToLower();

			switch (option) 
			{
				case "create":
					goto Create;
				case "delete":
					goto Delete;
				case "update":
					goto Update;
				case "getbyid":
					goto GetById;
				case "exit":
					goto Exit;
				default:
					Console.WriteLine("Unknow Command");
					goto Retry;

			}

        #region Create
        Create:
			Console.WriteLine("Create");

			var newMovie = new Movie();
			var newDirector = new Director();

			// Name
			Console.WriteLine("Enter Movie Name");
			newMovie.Name = Console.ReadLine();
			// Rating
			Console.WriteLine("Enter Rating");
			newMovie.Rating = Convert.ToDouble(Console.ReadLine());
			// Genre
			Console.WriteLine("Enter Genre : (Detective - 1, Fantasy - 2, Science_Fiction - 3, " +
				"Documentary - 4, Action - 5, Crime - 6, Drama - 7, Other - 0)");
			Console.WriteLine("Please Enter Number of Genre!");

			var genreNum = Convert.ToInt32(Console.ReadLine());

			switch (genreNum) 
			{
				case 1:
					newMovie.Genre = GenreEnum.Detective;
					break;
				case 2:
					newMovie.Genre = GenreEnum.Fantasy;
					break;
				case 3:
					newMovie.Genre = GenreEnum.Science_Fiction;
					break;
				case 4:
					newMovie.Genre = GenreEnum.Documentary;
					break;
				case 5:
					newMovie.Genre = GenreEnum.Action;
					break;
				case 6:
					newMovie.Genre = GenreEnum.Crime;
					break;
				case 7:
					newMovie.Genre = GenreEnum.Drama;
					break;
				default:
					newMovie.Genre = GenreEnum.Other;
					break;
			}
			// Director
			Console.WriteLine("Enter Director Name : ");
			newDirector.FullName = Console.ReadLine();

			newMovie.Director = newDirector;

			movieRepository.Create(newMovie);

			Console.WriteLine();

            goto Start;
		#endregion

		#region Delete
		Delete:
			Console.WriteLine("Delete");

			Console.WriteLine("Enter Objects 'Id' which you want to delete ");
			var id = Convert.ToInt32(Console.ReadLine());
			movieRepository.Delete(id);

			Console.WriteLine();

			goto Start;
        #endregion

        #region Update
        Update:
			Console.WriteLine("Update");

			Console.WriteLine("Select 'Id' which you want to update");
			var updateId = Convert.ToInt32(Console.ReadLine());

			Console.WriteLine("Write Field which you want to update");
		EnterFieldAgain:
			var field = Console.ReadLine().ToLower();

			var updateMovie = movies.Where(e => e.Id == updateId).FirstOrDefault();

			switch (field) 
			{
				case "name":
					Console.WriteLine("Enter New Name : ");
					updateMovie.Name = Console.ReadLine();
					break;
				case "rating":
					Console.WriteLine("Enter New Rating : ");
					updateMovie.Rating = Convert.ToDouble(Console.ReadLine());
					break;
				case "genre":
					Console.WriteLine("Enter New Genre : ");
					Console.WriteLine("Choose From Them : (Detective - 1, Fantasy - 2, Science_Fiction - 3, " +
				"Documentary - 4, Action - 5, Crime - 6, Drama - 7, Other - 0)");
					Console.WriteLine("Please Enter Number of Genre!");

					var genNum = Convert.ToInt32(Console.ReadLine());
					switch (genNum)
					{
						case 1:
							updateMovie.Genre = GenreEnum.Detective;
							break;
						case 2:
							updateMovie.Genre = GenreEnum.Fantasy;
							break;
						case 3:
							updateMovie.Genre = GenreEnum.Science_Fiction;
							break;
						case 4:
							updateMovie.Genre = GenreEnum.Documentary;
							break;
						case 5:
							updateMovie.Genre = GenreEnum.Action;
							break;
						case 6:
							updateMovie.Genre = GenreEnum.Crime;
							break;
						case 7:
							updateMovie.Genre = GenreEnum.Drama;
							break;
						default:
							updateMovie.Genre = GenreEnum.Other;
							break;
					}
					break;
				case "director":
					Console.WriteLine("Enter New Director Name : ");
					updateMovie.Director.FullName = Console.ReadLine();
					break;
				default:
					Console.WriteLine("Uncknow Field");
					goto EnterFieldAgain;
			}

			movieRepository.Update(updateMovie);

			Console.WriteLine();

			goto Start;
        #endregion

		#region GetById
		GetById:
			Console.WriteLine("GetById");
			Console.WriteLine("Write down 'Id' which movie info you want ");
			var getId = Convert.ToInt32(Console.ReadLine());

			var getMovie = movieRepository.GetById(getId);
			Print(getMovie);

			Console.WriteLine();
			goto Start;
			#endregion

			Exit:
			Console.WriteLine("Shutting Down");
			
        }

		// Print List
        private static void Print(List<Movie> movies)
		{
			foreach (var movie in movies)
			{
				Print(movie);
			}
		}

		//Overload Print for One Object print
		private static void Print(Movie movie)
		{
			Console.WriteLine($"Id: {movie.Id}; Movie Name: {movie.Name}; Rating: {movie.Rating}; " +
							  $"Genre: {GetGenre(movie.Genre)}; " +
							  $"Director: {movie.Director.FullName};");
			
		}

		// Custom function to get Genres from Object
		private static string GetGenre(GenreEnum genre)
		{
			switch (genre)
			{
				case GenreEnum.Action:
					return "Action";
				case GenreEnum.Crime:
					return "Cripe";
				case GenreEnum.Detective:
					return "Detective";
				case GenreEnum.Documentary:
					return "Documentary";
				case GenreEnum.Drama:
					return "Drama";
				case GenreEnum.Fantasy:
					return "Fantasy";
				case GenreEnum.Science_Fiction:
					return "Science Fiction";
				default: return "Unknown";
			}
		}

	}
}
