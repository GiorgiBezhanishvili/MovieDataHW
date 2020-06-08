using MovieData.Models;
using MovieData.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace MovieData.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies;
        private readonly List<Director> _directors;

        public MovieRepository()
        {
            _directors = _directors ?? new List<Director>()
            {
                new Director{ Id = 1,FullName = "Anthony Russo"},
                new Director{ Id = 2,FullName = "Christopher Nolan"},
                new Director{ Id = 3,FullName = "Joe Russo"},

            };

            _movies = _movies ?? new List<Movie>()
            {
                new Movie(){ Id = 1,Name = "Avengers: Infinity War",Rating = 8.5,Genre = Enums.GenreEnum.Fantasy,Director = _directors[0]},
                new Movie(){ Id = 2,Name = "The Dark Knight",Rating = 9.0,Genre = Enums.GenreEnum.Crime,Director = _directors[1]},
                new Movie(){ Id = 3,Name = "Captain America: Civil War",Rating = 7.8,Genre = Enums.GenreEnum.Action,Director = _directors[2]},
                new Movie(){ Id = 4,Name = "Interstellar",Rating = 8.6,Genre = Enums.GenreEnum.Science_Fiction,Director = _directors[1]},
            };
        }

        public void Create(Movie movie)
        {
            var lastId = _movies.Select(m => m.Id).LastOrDefault();
            movie.Id = ++lastId;

            var lasDirectorId = _directors.Select(e => e.Id).LastOrDefault();
            movie.Director.Id = ++lasDirectorId;

            _movies.Add(movie);
        }

        public void Delete(int id)
        {
            var movie = _movies.Where(e => e.Id == id).FirstOrDefault();
            _movies.Remove(movie);
        }

        public List<Movie> GetAll()
        {
            return _movies;
        }

        public Movie GetById(int id)
        {
            var data = _movies.Where(m => m.Id == id).FirstOrDefault();
            return data;
        }

        public void Update(Movie movie)
        {
            var id = movie.Id;
            var data = _movies.Where(m => m.Id == id).ToList();
            _movies.Remove(data.ElementAtOrDefault(0));
            _movies.Add(movie);
        }
    }
}
