using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YellowMovieProject.Data;
using YellowMovieProject.Models;
using YellowMovieProject.Models.ViewModels;
using YellowMovieProject.Services;

namespace YellowMovieProject.Controllers
{
    public class HomeController : Controller
    {
        MovieService movieService = new MovieService();
        OrderService orderService = new OrderService();
        public ActionResult Index()
        {
            HomeVM tops = new HomeVM();
            tops.Top5PopularMovies = movieService.MovieTop5Popular();
            tops.Top5NewestMovies = movieService.MovieTop5Newest();
            tops.Top5OldestMovies = movieService.MovieTop5Oldest();
            tops.Top5CheapestMovies=movieService.MovieTop5Cheapest();
            tops.ExpensiveOrder = orderService.MostExpensiveOrder();

            return View(tops);
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }
    }
}