using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YellowMovieProject.Models;
using YellowMovieProject.Models.ViewModels;
using YellowMovieProject.Services;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using YellowMovieProject.Models.Email;
using System.Net.Http;
using Newtonsoft.Json;

namespace YellowMovieProject.Controllers
{
    public class OrderController : Controller
    {
        OrderService orderService = new OrderService();
        MovieService movieService = new MovieService();

        private static readonly HttpClient httpClient = new HttpClient();


        // GET: Order
        public ActionResult OrderPlacement()
        {
            if (TempData["customerId"] != null && Session["shoppingList"] != null)
            {
                List<int> shoppingList = (List<int>)Session["shoppingList"];// reading shopping list ids from session storage
                int customerId = (int)TempData["customerId"];

                // we have to call order service to create new order
                OrderDetailVM newOrderDetail = new OrderDetailVM();
                newOrderDetail.Order = orderService.PlaceOrder(customerId, shoppingList);

                Session.Remove("shoppingList");
                Session.Remove("shoppingCount");

                foreach (OrderRow orderRow in newOrderDetail.Order.OrderRows)
                {
                    newOrderDetail.Total += orderRow.Quantity * orderRow.Price;
                }


                TempData["newOrderDetail"] = newOrderDetail;

                return RedirectToAction("RegisterOrder");
            }

            return View();
        }
        public ActionResult OrderDetails()
        {
            OrderDetailVM newOrderDetail = TempData["newOrderDetail"] as OrderDetailVM;
            ViewBag.Message = TempData["EmailStatus"];

            return View(newOrderDetail);
        }


        public async Task<ActionResult> RegisterOrder()
        {

            OrderDetailVM newOrderDetail = TempData["newOrderDetail"] as OrderDetailVM;

            Order registerOrder = newOrderDetail.Order;

            double total = 0;

            string custName = registerOrder.Customer.FirstName + " "
                + registerOrder.Customer.LastName;

            ConfirmMessage sendMsg = new ConfirmMessage()
            {
                Name = custName,
                Email = registerOrder.Customer.EmailAddress,
                Subject = "Order acknowledgement,order #" + registerOrder.Id + "dated " + registerOrder.OrderDate.ToString("yyyy-MM-dd"),
                EmailPlain = " Dear customer, Thank you for ordering from Yelow Movie Store. \n\n" +
                " Your customer number:  " + registerOrder.CustomerId + "    Your order number:  " + registerOrder.Id + "  \n\n" +
                " We try to do everything we can to meet your expectations. We hope you are satisfied and will continue to order from us in the future. \n\n" +
                " You have ordered the following items: \n" +
                "Product name	|	Price per unit	|	Quantity	|	Amount",

                EmailHtml = "<p>Dear customer, Thank you for ordering from Yelow Movie Store.</p>" +
                "<p>Your customer number: <b>" + registerOrder.CustomerId + "</b></p> <p>Your order number: <b>" + registerOrder.Id + "</b></p>" +
                "<p>We try to do everything we can to meet your expectations. We hope you are satisfied and will continue to order from us in the future.</p>" +
                "<p>You have ordered the following items:</p>" +
                "<table width='100%'> <tbody> <tr> <th > Product name </th> <th> Price per unit </th> <th> Quantity </th> <th> Amount </th> </tr>"
            };

            foreach (var item in registerOrder.OrderRows)
            {
                Movie movie = movieService.MovieFind(item.MovieId);
                sendMsg.EmailPlain += movie.Title + "  |   $" + item.Price + " |   " + item.Quantity + "   |   $" + item.Price * item.Quantity + "\n";
                sendMsg.EmailHtml += "<tr> <td> <p>" + movie.Title + "</p> </td> <td> $" + item.Price + " </td> <td> " + item.Quantity + "</td> <td> $" + item.Price * item.Quantity + " </td> </tr>";

                total += item.Price * item.Quantity;
            }

            sendMsg.EmailPlain += "Total amount:$" + total + "\n\n" +
                          "Delivery:  Standard shipping\n\n" +
                          "Your billing address:" + custName + "\n" + registerOrder.Customer.BillingAddress + "\n" + registerOrder.Customer.BillingZIP + ", " + registerOrder.Customer.BillingCity + "\n\n" +
                          "Your shipping address:\n\n" + custName + "\n" + registerOrder.Customer.DeliveryAddress + "\n" + registerOrder.Customer.DeliveryZIP + ", " + registerOrder.Customer.DeliveryCity;

            sendMsg.EmailHtml += "<tr> <td colspan='3'> <b>Total amount: </b> </td> <td> <b>$" + total + " </b> </td> </tr>" +
              "<tr> <td colspan='4'> <p>Delivery: <b>Standard shipping</b></p> </td> </tr>" +
              "<tr> <th colspan='2'> Your billing address: </th> <th colspan='2'> Your shipping address: </th> </tr>" +
              "<tr> <td colspan='2'> <p>" + custName + "<br/>" + registerOrder.Customer.BillingAddress + "<br/>" + registerOrder.Customer.BillingZIP + ", " + registerOrder.Customer.BillingCity + " </p> </td>" +
              "<td colspan='2'> <p>" + custName + "<br/>" + registerOrder.Customer.DeliveryAddress + "<br/>" + registerOrder.Customer.DeliveryZIP + ", " + registerOrder.Customer.DeliveryCity + "</p> </td> </tr> </tbody> </table>";




            var statusMessage = await SendConfirmation(sendMsg);
            TempData["EmailStatus"] = statusMessage;

            return RedirectToAction("OrderDetails");


        }

        private async Task<string> SendConfirmation(ConfirmMessage sendMsg)
        {
            //Azure Funtion Link
            var myEmailFunctionUrl = "https://yellowemaildelivery.azurewebsites.net/api/EmailToQueue?code=w8ZBSiUmQtNexxHrWlDResa6onZzIC2ZBE6ClWRgO4tagEiFaW6zOg==";

            //local function link
            var myLocalEmailFunctionUrl = "http://localhost:7071/api/EmailToQueue";

            var functionUrl = myEmailFunctionUrl; //change when mail client is ready
            string statusMsg = "";


            using (var myRequest = new HttpRequestMessage(HttpMethod.Post, functionUrl))

            {

                string jsonString = JsonConvert.SerializeObject(sendMsg);
                StringContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                myRequest.Content = httpContent;
                HttpResponseMessage newResponse = await httpClient
                                                    .SendAsync(myRequest)
                                                    .ConfigureAwait(false);

                if (newResponse.IsSuccessStatusCode)
                {
                    statusMsg = "Your order has been registered. A confirmation has been sent by email to:";
                }
                else
                {
                    statusMsg = "Something went wrong.";
                }


            };
            return statusMsg;
        }
    }
}