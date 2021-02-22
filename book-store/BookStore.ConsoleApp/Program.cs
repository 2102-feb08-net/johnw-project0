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
            var hs = new HelperService(new Repository());
            var x = hs.GetAllCustomers();
            foreach(var c in x)
            {
                Console.WriteLine($"{c.FirstName} {c.LastName}");
            }

            var y = hs.GetLocationByID(1);
            Console.WriteLine($"\nInventory for the {y.Name}");
            foreach(var i in y.Inventory)
            {
                Console.WriteLine($"{i.Key.Name}\t{i.Value}");
            }

            var z = hs.GetAllLocations();
            Console.WriteLine("\nHere are all the locations");
            foreach(var i in z)
            {
                Console.WriteLine($"{i.Name}\t{i.Inventory.Count}");
            }


            var a = hs.GetAllOrders();
            Console.WriteLine("\nHere are all of the orders:");
            foreach(var i in a)
            {
                Console.WriteLine($"{i.CustomerID} - {i.LocationID} - {i.Items.Count}");
            }
        }
    }
}
