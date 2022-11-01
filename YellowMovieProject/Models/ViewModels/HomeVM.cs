using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YellowMovieProject.Models.ViewModels
{
    public class HomeVM
    {
        public List<Movie> Top5PopularMovies { get; set; }
        public List<Movie> Top5NewestMovies { get; set; }
        public List<Movie> Top5OldestMovies { get; set; }
        public List<Movie> Top5CheapestMovies { get; set; }
        public OrderDetailVM ExpensiveOrder { get; set; }

        public HomeVM()
        { 
            Top5PopularMovies= new List<Movie>();   
            Top5NewestMovies= new List<Movie>();
            Top5CheapestMovies = new List<Movie>();
            Top5OldestMovies = new List<Movie>();
        }


    }
}