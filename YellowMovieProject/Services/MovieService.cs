using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YellowMovieProject.Data;
using YellowMovieProject.Models;

namespace YellowMovieProject.Services
{
    public class MovieService
    {
        readonly  ApplicationDbContext _db;

        public MovieService()
        {
        _db = new ApplicationDbContext();
        }

        public  Boolean MovieAdd(Movie newMovie) // add given newMovie into Movies table
        {
            _db.Movies.Add(newMovie);
            if (_db.SaveChanges() > 0)
                return true;
            else return false;
        }

        public List<Movie> MovieList() // return list of movies ordered by latest added
        {
            var result = _db.Movies.OrderByDescending(m => m.Id).ToList();
            return result;
        }

        public List<Movie> MovieList(MoviePaginationHelper MPH) // return list of movies ordered by latest added
        {
            int Maxresults = 10;
            MPH.Movies = _db.Movies
                    .OrderByDescending(m => m.Id)
                   .Skip((MPH.CurrentPageIndex - 1)*Maxresults)
                   .Take(Maxresults)
                   .ToList();
            return MPH.Movies;
        }


        public List<Movie> SearchMovie(string search) // return list of movies ordered by latest added
        {
            var result = _db.Movies.Where(m=>m.Title.Contains(search)).ToList();
            return result;
        }

        public  Movie MovieFind(int? id) // return Movie with given Id
        {
            return _db.Movies.Find(id);     
        }

        public  Boolean MovieDelete(int? id) //Delete Movie with given Id
        {
            if (id == null) return false;
            else
            {
            Movie movie = _db.Movies.Find(id);
            _db.Movies.Remove(movie);
                if (_db.SaveChanges() > 0)
                    return true;
                else 
                    return false;
            }
            
        }

        public  Boolean MovieUpdate(Movie newMovie) // update/change Movie to newMovie
        {
    
            _db.Entry(newMovie).State = EntityState.Modified;
            if (_db.SaveChanges() > 0)
                return true;
            else
                return false;
        }
        public List<Movie> MovieTop5Newest() // return list of 5 top newest movies.
        {
            var list = _db.Movies.OrderByDescending (ry => ry.ReleaseYear).Take(5).ToList();
            return list;
        }
        public List<Movie> MovieTop5Oldest() // return list of 5 top oldest movies.
        {
            var list = _db.Movies.OrderBy (ry => ry.ReleaseYear).Take(5).ToList();
            return list;
        }

        public List<Movie> MovieTop5Cheapest() // return list of 5 top cheapest movies.
        {
            var list = _db.Movies.OrderBy(pr => pr.Price).Take(5).ToList();
            return list;
        }

        public List<Movie> MovieTop5Popular()
        {
            var polular = (from ordRows in _db.OrderRows
                        group ordRows by ordRows.MovieId into oRows
                        select new
                        {
                            MovieId = oRows.Key,
                            Quantity = oRows.Sum(o => o.Quantity)
                        })
                        .Take(5)
                        .OrderByDescending(o => o.Quantity) 
                        .ToList();
            List<Movie> list = new List<Movie>();

            foreach(var row in polular)
            {
                list.Add(MovieFind(row.MovieId));
            }
            return list;
        }

        
    }
}