using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YellowMovieProject.Data;
using YellowMovieProject.Models;
using YellowMovieProject.Models.ViewModels;

namespace YellowMovieProject.Services
{
    public class OrderService
    {
        readonly ApplicationDbContext _db;
        public OrderService()
        {
            _db = new ApplicationDbContext();
        }

        public Order PlaceOrder(int customer, List<int> shoppingList)
        {

            Order newOrder = new Order();

            newOrder.Customer = _db.Customers.Find(customer);//customerService.CustomerFind(customer.Id);

            if (shoppingList != null) // if shopping list is not empty
            {
                foreach (int itemId in shoppingList)
                {
                    var movieInList = newOrder.OrderRows.FirstOrDefault(m => m.Movie.Id == itemId);//if this movie is already in the list
                    if (movieInList != null)// it is there
                    {
                        movieInList.Quantity++;// just increase its quantity
                    }
                    else
                    {
                        newOrder.OrderRows.Add(new OrderRow
                        {
                            Movie = _db.Movies.Find(itemId),
                            Quantity = 1,
                            Price = _db.Movies.Find(itemId).Price
                        });
                    }
                }
            }


            _db.Orders.Add(newOrder);


            _db.SaveChanges();

            return newOrder;
        }


        public Order GetLatestCustOrder()
        {

            Order latestOrder = _db.Orders
                //.Where(o => o.CustomerId == Id)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefault();

            return latestOrder;
        }



        public List<OrderDetailVM> OrderList(Customer inCust) //Return existing list of orders, newest first
        {
            List<OrderDetailVM> listDetail= new List<OrderDetailVM>();
            if (inCust != null) { 
            var list = _db.Orders.Where(n => n.CustomerId.Equals(inCust.Id)).OrderByDescending(m => m.Id).ToList();
            foreach (var order in list)
            {
                listDetail.Add(new OrderDetailVM()
                {
                    Order = order,
                    Total = order.OrderRows.Sum(or=>or.Quantity*or.Price)
                });
            }
            }
            return listDetail;
        }
        public Order OrderFind(int orderId)
        {

            return _db.Orders.Find(orderId);
        }


        public OrderDetailVM MostExpensiveOrder()
        {
            var expOrId =
                (from oR in _db.OrderRows
                 group oR by oR.OrderId into newGroup
                 select new
                 {
                     OrderId = newGroup.Key,
                     Total = newGroup.Sum(x => x.Quantity * x.Price)
                 }).OrderByDescending(x => x.Total).FirstOrDefault();

            OrderDetailVM expensiveOrder = new OrderDetailVM();

            if (expOrId != null)
            {
                expensiveOrder.Order = OrderFind(expOrId.OrderId);
                expensiveOrder.Total = expOrId.Total;
            }

            return expensiveOrder;

        }

    }
}