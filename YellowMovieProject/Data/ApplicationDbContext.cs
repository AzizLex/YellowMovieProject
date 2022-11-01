using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YellowMovieProject.Models;

namespace YellowMovieProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext():base("name=DefaultConnection")
        {

        }
        public DbSet <Customer> Customers { get; set; }
        public DbSet <Movie> Movies { get; set; }
        public DbSet <Order> Orders { get; set; }
        public DbSet <OrderRow> OrderRows { get; set; }

    
    }
}