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
    public class CustomerController : Controller
    {

        CustomerService customerService = new CustomerService();

        OrderService ordServ = new OrderService();


        [HttpGet] 
        public ActionResult AddCustomer()
        {
            Customer addCust = new Customer();
            return View(addCust);
        }
        
        [HttpPost]
        public ActionResult AddCustomer(Customer model)
        {
            if (ModelState.IsValid)
            {
                if (model.DeliveryAddress == null && model.DeliveryZIP == null && model.DeliveryCity == null)

                {
                    model.DeliveryAddress = model.BillingAddress;
                    model.DeliveryZIP = model.BillingZIP;
                    model.DeliveryCity = model.BillingCity;
                }
                else if (model.DeliveryAddress == null || model.DeliveryZIP == null || model.DeliveryCity == null)
                {
                    ViewBag.CustomerFormError = "Fill delivery address";
                    return View(model);
                }

                customerService.CustomerAdd(model);

                ModelState.Clear();
                Customer customer = new Customer()
                {
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    PhoneNo = string.Empty,
                    EmailAddress = string.Empty,
                    BillingAddress = string.Empty,
                    BillingCity = string.Empty,
                    BillingZIP = string.Empty,  
                    DeliveryAddress=String.Empty,
                    DeliveryCity=String.Empty,
                    DeliveryZIP=String.Empty
                };
                model = customer;
                ViewBag.AddCustomerSuccessMessage = "Customer added successfully.";
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AddDetail()
        {
            Customer addCust = new Customer();
            return View(addCust);
        }


        [HttpPost]
        public ActionResult AddDetail(Customer model)

        {
            if (ModelState.IsValid)
            {
                if (model.DeliveryAddress == null && model.DeliveryZIP == null && model.DeliveryCity == null)

                {
                    model.DeliveryAddress = model.BillingAddress;
                    model.DeliveryZIP = model.BillingZIP;
                    model.DeliveryCity = model.BillingCity;
                }
                else if (model.DeliveryAddress == null || model.DeliveryZIP == null || model.DeliveryCity == null)
                {
                    ViewBag.CustomerFormError = "Fill delivery address";
                    return View(model);
                }

                customerService.CustomerAdd(model);
                TempData["customerId"] = model.Id;
                return RedirectToAction("OrderPlacement", "Order");
            }
            return View(model);
        }
        // ---> Ask for a customer email, then display one customers order history<---
        
        [HttpGet] 
        public ActionResult CustomerOrders(String inMail)
        {
            List<OrderDetailVM> list = new List<OrderDetailVM>();
            Customer customer = customerService.CustomerFind(inMail);
            if(customer != null) 
            { 
            list.AddRange(ordServ.OrderList(customer));
            }
            return View(list);
        }
    }
}