using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YellowMovieProject.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "{0} has min 1 and max 100 characters")]
        public string Title { get; set; }


        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "{0} has min 2 and max 100 characters")]
        public string Director { get; set; }


        [Display(Name = "Release year")]
        [Range(1888, 2023, ErrorMessage = "{0} is between 1888 and 2023.")]
        [Required(ErrorMessage = "{0} is required.")]
        public int ReleaseYear { get; set; }


        [Required(ErrorMessage = "{0} is required.")]
        [Range(1, 1999, ErrorMessage = "{0} should be between 1 and 1999")]
        public double Price { get; set; }


        [Display(Name = "Poster URL")]
        public string PosterURL { get; set; } //Poster image suffix https://image.tmdb.org/t/p/w400/

        public virtual ICollection<OrderRow> OrderRows { get; set; } //For finding orderrows containing certain movies (receipts, etc.)

        public Movie()
        {
            OrderRows = new List<OrderRow>();

        }
    }
}