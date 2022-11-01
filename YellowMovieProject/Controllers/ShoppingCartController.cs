using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YellowMovieProject.Models;
using YellowMovieProject.Services;

namespace YellowMovieProject.Controllers
{

    public class ShoppingCartController : Controller

    {
        CustomerService customerService = new CustomerService();
        MovieService movieService = new MovieService();
        OrderService orderService = new OrderService();

        //Class to receive MovieId from Add to Cart button
        public class MovieId
        {
            public int Id { get; set; }
        }

        //Class to receive most updated list from Shopping Cart Index view
        public class shopList
        {
            public List<int> MovieIds { get; set; }
            public List<int> Quantities { get; set; }

        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<int> shoppingList = (List<int>)Session["shoppingList"];
            List<OrderRow> cartRows = new List<OrderRow>();
            double total = 0;

            if (shoppingList != null)
            {
                foreach (int itemId in shoppingList)
                {
                    var movieInList = cartRows.Find(m => m.MovieId == itemId);
                    if (movieInList != null)
                    {
                        movieInList.Quantity++;
                        total += movieInList.Price;
                    }
                    else
                    {
                        OrderRow shoppingItem = new OrderRow();
                        shoppingItem.MovieId = itemId;
                        shoppingItem.Movie = movieService.MovieFind(itemId);
                        shoppingItem.Quantity = 1;
                        shoppingItem.Price = shoppingItem.Movie.Price;
                        total += shoppingItem.Price;
                        cartRows.Add(shoppingItem);
                    }

                }
            }

            ViewBag.Total = total;

            return View(cartRows);
        }

        // Action to receive Ajax request from Add to Cart button event 
        [HttpPost]
        public ActionResult PostFromAjax(MovieId movieId)
        {
            if (Session["shoppingList"] == null)
            {
                List<int> shoppingList = new List<int>();
                shoppingList.Add(movieId.Id);
                Session["shoppingList"] = shoppingList;
                Session["shoppingCount"] = 1;
            }
            else
            {
                List<int> shoppingList = (List<int>)Session["shoppingList"];
                shoppingList.Add(movieId.Id);
                Session["shoppingList"] = shoppingList;
                Session["shoppingCount"] = shoppingList.Count;
            }

            return Json(new { Value = Session["shoppingCount"] });
        }

        //Action to receive updated Shopping Cart items from Shopping Cart index View
        //and create shopping cart list in Session object
        [HttpPost]
        public ActionResult updateCart(shopList shopList)
        {
            if (shopList != null)
            {
                List<int> shoppingList = new List<int>();
                for (int i = 0; i < shopList?.MovieIds?.Count; i++)
                {
                    for (int j = 0; j < shopList.Quantities[i]; j++)
                    {
                        shoppingList.Add(shopList.MovieIds[i]);
                    }
                }
                Session["shoppingList"] = shoppingList;
                Session["shoppingCount"] = shoppingList.Count;
                return Json(new { Value = Session["shoppingCount"] });
            }
            else
            {
                Session.Contents.Clear();
                return Json(new { Value = 0 });
            }

        }


        //Action to check if user exists by checking its email or phone number
        public ActionResult checkUserContact(string contact)
        {
            contact = contact.ToLower().Trim();
            if (contact == null || contact.Length == 0)
            {
                return RedirectToAction("AddDetail", "Customer");
            }

            Customer customer = customerService.CustomerFind(contact);
            if (customer == null)
            {
                return RedirectToAction("AddDetail", "Customer");
            }

            TempData["customerId"] = customer.Id;

            return RedirectToAction("OrderPlacement", "Order");
        }

    }
}