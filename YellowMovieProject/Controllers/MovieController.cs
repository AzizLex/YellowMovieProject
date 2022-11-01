using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YellowMovieProject.Data;
using YellowMovieProject.Models;
using YellowMovieProject.Services;

namespace YellowMovieProject.Controllers
{
    public class MovieController : Controller
    {

        MovieService movieService = new MovieService();

        // GET: Movie
        public ActionResult Index()
        {
            Order newOrder = new Order();

            return View();
        }
        public ActionResult ListAllMovies(string currentPage)
        {
            if (currentPage == null || currentPage == "")
            {
                return View(this.GetMPH(1));
            }
            else
            {

                return View(this.GetMPH(int.Parse(currentPage)));
            }

        }

        //[HttpPost]
        //public ActionResult ListAllMovies(int? currentPage)
        //{            
        //    return View(this.GetMPH(currentPage));
        //}

        private MoviePaginationHelper GetMPH(int? currentPage)
        {
            int maxResults = 10;
            MoviePaginationHelper MPH = new MoviePaginationHelper();

            MPH.CurrentPageIndex = currentPage ?? 1;
            MPH.Movies = movieService.MovieList(MPH);

            MPH.PageCount = (int)Math.Ceiling((double)(movieService.MovieList().Count()) / Convert.ToDouble(maxResults));

            return MPH;
        }
        public ActionResult SearchMovies(string search)
        {
            return View(movieService.SearchMovie(search));
        }

        [HttpGet] //HttpGet is implicated if empty
        public ActionResult AddMovie()
        {
            Movie addMov = new Movie();
            return View(addMov);
        }

        [HttpPost]
        public ActionResult AddMovie(Movie model)
        {
            if (ModelState.IsValid)
            {
                movieService.MovieAdd(model);
                ModelState.Clear();
                Movie movie = new Movie()
                {
                    Title = string.Empty,
                    Director = string.Empty,
                    ReleaseYear = 0,
                    Price = 0,
                    PosterURL = string.Empty
                };
                model = movie;
                ViewBag.AddMovieSuccessMessage = "Movie added successfully.";
            }
            return View(model);
        }
    }
}