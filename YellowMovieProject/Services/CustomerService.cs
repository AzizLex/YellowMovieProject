using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YellowMovieProject.Data;
using YellowMovieProject.Models;

namespace YellowMovieProject.Services
{
    public class CustomerService
    {
        readonly ApplicationDbContext _db;

        public CustomerService()
        {
            _db = new ApplicationDbContext();
        }

        public Boolean CustomerAdd(Customer newCustomer) // add given newCustomer into Customer table
        {
            _db.Customers.Add(newCustomer);
            if (_db.SaveChanges() > 0)
                return true;
            else return false;
        }


        public List<Customer> CustomerList() // return list of customers ordered by latest added
        {
            var result = _db.Customers.OrderByDescending(c => c.Id).ToList();
            return result;
        }

        public Customer CustomerFind(int? id) // return Customer with given Id
        {
            return _db.Customers.Find(id);
        }

        public Customer CustomerFind(string contact) // return Customer with given email or phone
        {
            Customer customer = new Customer();
            if (contact != null && contact != "")
            {
                customer = _db.Customers.FirstOrDefault(c => c.EmailAddress == contact);
                if (customer == null)
                {
                    customer = _db.Customers.FirstOrDefault(c => c.PhoneNo == contact);
                }
            }
            return customer;
        }

        public Boolean CustomerDelete(int? id) //Delete Customer with given Id
        {
            if (id == null) return false;
            else
            {
                Customer Customer = _db.Customers.Find(id);
                _db.Customers.Remove(Customer);
                if (_db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }

        }

        public Boolean CustomerUpdate(Customer newCustomer) // update/change Customer to §newCustomer
        {

            _db.Entry(newCustomer).State = EntityState.Modified;
            if (_db.SaveChanges() > 0)
                return true;
            else
                return false;
        }


    }
}


