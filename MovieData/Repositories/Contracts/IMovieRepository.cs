using MovieData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieData.Repositories.Contracts
{
    public interface IMovieRepository
    {
        Movie GetById(int id);
        List<Movie> GetAll();
        void Create(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
    }
}
