using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YellowMovieProject.Models
{
    public class MoviePaginationHelper
    {
        public List<Movie> Movies { get; set; }

        public int CurrentPageIndex { get; set; }

        public int PageCount { get; set; }
    }
}