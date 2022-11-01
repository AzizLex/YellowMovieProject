using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YellowMovieProject.Models.Email
{
    public class ConfirmMessage
    {

        public string Name { get; set; }
        public string Email { get; set; } 
        public string Subject { get; set; }
        public string EmailPlain { get; set; }
        public string EmailHtml { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public ConfirmMessage()
        {
            OrderItems = new List<OrderItem>();
        }

    }
}