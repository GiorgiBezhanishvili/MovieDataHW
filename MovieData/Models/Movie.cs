using MovieData.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieData.Models
{
    public class Movie
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public double Rating { get; set; }
        public GenreEnum Genre { get; set; }
        public Director Director { get; set; }
    }
}
