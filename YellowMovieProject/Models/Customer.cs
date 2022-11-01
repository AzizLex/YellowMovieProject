using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YellowMovieProject.Models
{
    public class Customer
    {

        public int Id { get; set; }

        //[Required, StringLength(50), Display(Name ="First name")]
        [Display(Name = "First name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessage = "{0} has min 1 and max 50 characters")]
        public string FirstName { get; set; }

        
        //[Required,StringLength(50), Display(Name = "Last name")]
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} has min 1 and max 50 characters")]
        public string LastName { get; set;}

        


        //[Required, StringLength(10), Display(Name = "Billing address")]
        [Display(Name = "Billing Address")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} has min 1 and max 50 characters")]
        public string BillingAddress { get; set; }


        //[Required, StringLength(50), Display(Name = "Billing ZIP")]
        [Display(Name = "Billing ZIP")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(maximumLength: 10, MinimumLength = 1, ErrorMessage = "{0} has min 1 and max 10 characters")]
        public string BillingZIP { get; set; }




        //[Required, StringLength(50), Display(Name = "Billing city")]
        [Display(Name = "Billing city")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} has min 1 and max 50 characters")]
        public string BillingCity { get; set; }



        //[StringLength(50), Display(Name = "Delivery address")]
        [Display(Name = "Delivery address")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} has min 1 and max 50 characters")]
        public string DeliveryAddress { get; set; }



        //[StringLength(10), Display(Name = "Delivery ZIP")]
        [Display(Name = "Delivery ZIP")]
        [StringLength(maximumLength: 10, MinimumLength = 1, ErrorMessage = "{0} has min 1 and max 50 characters")]
        public string DeliveryZIP { get; set; }
        
        
        
        
        
        //[StringLength(50) , Display(Name = "Delivery city")]
        [Display(Name = "Delivery city")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "{0} has min 1 and max 50 characters")]
        public string DeliveryCity { get; set; }
        
        
        
        //[StringLength(50), Display(Name = "Email address")]
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set;}
        
        
        
        //[StringLength(20), Display(Name = "Phone number")]
        [Display(Name = "Phone number")]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "{0} has min 5 and max 20 characters")]
        public string PhoneNo { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }
        public Customer()
        {
            Orders=new List<Order>();
        }
       
        
        

    }
    
}