using BookStore.DataAccess;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var cs = new CustomerService();
            var x = cs.GetAllCustomers();
            Console.WriteLine(x);

            var c = cs.GetCustomerByFullName("Joe Schmo");
            c.DefaultLocationID = 3;
            cs.UpdateCustomer(c);
            cs.DeleteCustomer(c);
        }
    }
}
