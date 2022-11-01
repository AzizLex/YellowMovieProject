using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YellowMovieProject.Models
{
    public class OrderRow
    {

        [Required]
        public int Id { get; set; }
        
        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F20}")]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public OrderRow()
        {

        }
        
    }
}