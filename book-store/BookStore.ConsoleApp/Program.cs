using BookStore.DataAccess;
using BookStore.Services;
using Figgle;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FiggleFonts.Train.Render(" BookStore"));
            var hs = new HelperService(new Repository());
            string input = "";
            do
            {
                Console.WriteLine("\nType the letter corresponding to your choice found below:");
                Console.WriteLine("c : View/add customers");
                Console.WriteLine("h : View order histories");
                Console.WriteLine("p : Place a new order");
                Console.WriteLine("q : Quit out of the program");
                input = "";
                input = Console.ReadLine();
                if (char.IsLetter(input[0]))
                {
                    char userInput = input[0];
                    switch(userInput)
                    {
                        case 'c':
                            // go to view/add customers
                            ViewAddCustomers(hs);
                            break;
                        case 'h':
                            // go to view order histories
                            ViewOrderHistories(hs);
                            break;
                        case 'p':
                            // go to place a new order
                            PlaceANewOrder(hs);
                            break;
                        default:
                            break;
                    }
                } 
                else
                {
                    Console.WriteLine("Invalid input given. Please type the letter next to your corresponding choice.");
                }
            } while (input != "q");
            Environment.Exit(0);
        }

        /// <summary>
        /// Allows the user to view the list of customers and add new ones to the list, and/or they can return up a level or exit the program.
        /// </summary>
        /// <param name="hs"></param>
        static void ViewAddCustomers(HelperService hs)
        {
            string input = "";
            do
            {
                Console.WriteLine("\nType the letter corresponding to your choice found below:");
                Console.WriteLine("v : View list of customers");
                Console.WriteLine("a : Add a new customer");
                Console.WriteLine("b : Go back a menu");
                Console.WriteLine("q : Quit out of the program");
                input = "";
                input = Console.ReadLine();
                if (char.IsLetter(input[0]))
                {
                    char userInput = input[0];
                    switch (userInput)
                    {
                        case 'v':
                            Console.WriteLine("\nFirst Name\tLast Name");
                            Console.WriteLine("-------------------------");
                            List<Domain.Customer> customers = hs.GetAllCustomers();
                            foreach (var c in customers)
                            {
                                Console.WriteLine($"{c.FirstName}\t\t{c.LastName}");
                            }
                            break;
                        case 'a':
                            bool valid = false;
                            string first = "";
                            string last = "";
                            
                            do
                            {
                                Console.WriteLine("\nEnter the first name of the new customer:");
                                first = Console.ReadLine();
                                valid = first.All(Char.IsLetter);
                                if (!valid)
                                {
                                    Console.WriteLine("\nInvalid name entered. Please enter a name with only alphabetic characters.");
                                }
                            } while (!valid);
                            
                            valid = false;
                            
                            do
                            {
                                Console.WriteLine("\nEnter the last name of the new customer:");
                                last = Console.ReadLine();
                                valid = last.All(Char.IsLetter);
                                if (!valid)
                                {
                                    Console.WriteLine("\nInvalid name entered. Please enter a name with only alphabetic characters.");
                                }
                            } while (!valid);

                            hs.AddCustomer(new Domain.Customer(first, last));
                            Console.WriteLine($"New customer {first} {last} added.");
                            break;
                        case 'b':
                            // go up a level in the menu
                            return;
                        default:
                            break;
                    }
                }
            } while (input != "q");
            Environment.Exit(0);
        }

        /// <summary>
        /// Allows the user to select what type of order history to view, and/or they can return up a level or exit the program.
        /// </summary>
        /// <param name="hs"></param>
        static void ViewOrderHistories(HelperService hs)
        {
            string input = "";
            do
            {
                Console.WriteLine("\nType the letter corresponding to your choice found below:");
                Console.WriteLine("c : View order history of a customer");
                Console.WriteLine("l : View order history of a location");
                Console.WriteLine("b : Go back a menu");
                Console.WriteLine("q : Quit out of the program");
                input = "";
                input = Console.ReadLine();
                if (char.IsLetter(input[0]))
                {
                    char userInput = input[0];
                    switch (userInput)
                    {
                        case 'c':
                            ViewCustomerOrderHistory(hs);
                            break;
                        case 'l':
                            ViewLocationOrderHistory(hs);
                            break;
                        case 'b':
                            // go up a level in the menu
                            return;
                        default:
                            break;
                    }
                }
            } while (input != "q");
            Environment.Exit(0);
        }

        /// <summary>
        /// Allows the user to view the order history of a particular customer. Shows the list of customers again as a reference.
        /// </summary>
        /// <param name="hs"></param>
        static void ViewCustomerOrderHistory(HelperService hs)
        {
            List<Domain.Customer> customers = hs.GetAllCustomers();
            Console.WriteLine("\nEnter the ID of the customer you would like to view the order history for:");
            Console.WriteLine("ID | First Name\tLastName");
            Console.WriteLine("-------------------------");
            foreach(var c in customers)
            {
                Console.WriteLine($"{c.ID}  | {c.FirstName}\t{c.LastName}");
            }
            string input = "";
            bool valid = false;
            do
            {
                input = "";
                input = Console.ReadLine();
                valid = char.IsDigit(input[0]);
                if (valid)
                {
                    int i = input[0] - '0';
                    valid = customers.Any(c => c.ID == i);
                    if (valid)
                    {
                        List<Domain.Order> orders = hs.GetCustomerOrderHistory(i);
                        if (orders.Count > 0)
                        {
                            Console.WriteLine("{0, -3} | {1, -30} | {2, -15}", "ID", "Time", "Total");
                            Console.WriteLine("-----------------------------------------------");
                            foreach (var o in orders)
                            {
                                Console.WriteLine($"{o.ID, -3} | {o.Time, -30} | ${Math.Round(o.Total, 2), -15}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This customer has not placed any orders yet.");
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID number. Please choose an ID from the list");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID number. Please choose an ID from the list");
                }
            } while (!valid);
        }

        /// <summary>
        /// Allows the user to view the order history for a certain location. Displays the list of locations as well as a reference.
        /// </summary>
        /// <param name="hs"></param>
        static void ViewLocationOrderHistory(HelperService hs)
        {
            List<Domain.Location> locations = hs.GetAllLocations();
            Console.WriteLine("\nEnter the ID of the location you would like to view the order history for:");
            Console.WriteLine("ID | Name");
            Console.WriteLine("-------------------------");
            foreach (var l in locations)
            {
                Console.WriteLine($"{l.ID}  | {l.Name}");
            }
            string input = "";
            bool valid = false;
            do
            {
                input = "";
                input = Console.ReadLine();
                valid = char.IsDigit(input[0]);
                if (valid)
                {
                    int i = input[0] - '0';
                    valid = locations.Any(l => l.ID == i);
                    if (valid)
                    {
                        List<Domain.Order> orders = hs.GetLocationOrderHistory(i);
                        if (orders.Count > 0)
                        {
                            Console.WriteLine("{0, -3} | {1, -30} | {2, -15}", "ID", "Time", "Total");
                            Console.WriteLine("-----------------------------------------------");
                            foreach (var o in orders)
                            {
                                Console.WriteLine($"{o.ID,-3} | {o.Time, -30} | ${Math.Round(o.Total, 2), -15}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This location has not had any orders placed yet.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid ID number. Please choose an ID from the list");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID number. Please choose an ID from the list");
                }
            } while (!valid);
        }

        /// <summary>
        /// Allows the user to begin constructing and placing an order of products from one of the locations.
        /// </summary>
        /// <param name="hs"></param>
        static void PlaceANewOrder(HelperService hs)
        {
            Console.WriteLine("Please wait while we get things ready...");
            List<Domain.Customer> customers = hs.GetAllCustomers();
            List<Domain.Location> locations = hs.GetAllLocations();
            string input = "";
            bool valid = false;
            do
            {
                Console.WriteLine("\nEnter the ID of the customer placing this order:");
                Console.WriteLine("l : View list of customers");
                Console.WriteLine("b : Go back a menu");
                Console.WriteLine("q : Quit out of the program");
                input = "";
                input = Console.ReadLine();
                valid = Char.IsLetter(input[0]);
                if (valid)
                {
                    char userInput = input[0];
                    switch (userInput)
                    {
                        case 'l':
                            Console.WriteLine("ID | First Name\tLastName");
                            Console.WriteLine("-------------------------");
                            foreach (var c in customers)
                            {
                                Console.WriteLine($"{c.ID}  | {c.FirstName}\t{c.LastName}");
                            }
                            valid = false;
                            break;
                        case 'b':
                            // go up a level in the menu
                            return;
                        case 'q':
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
                else if(char.IsDigit(input[0]))
                {
                    int i = input[0] - '0';
                    valid = customers.Any(c => c.ID == i);
                    if (valid)
                    {
                        PlaceOrderChooseLocation(hs, locations, i);
                    }
                    else
                    {
                        Console.WriteLine("Invalid customer ID number. Please enter another number.");
                    }
                }
            } while (!valid);
            Environment.Exit(0);
        }

        /// <summary>
        /// Allows the user to choose the location that they will order from, now that the customer has been chosen.
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="locations"></param>
        /// <param name="customerID"></param>
        static void PlaceOrderChooseLocation(HelperService hs, List<Domain.Location> locations, int customerID)
        {
            string input = "";
            bool valid = false;
            do
            {
                Console.WriteLine("\nEnter the ID of the store location for this order:");
                Console.WriteLine("l : View list of store locations");
                Console.WriteLine("b : Go back a menu");
                Console.WriteLine("q : Quit out of the program");
                input = "";
                input = Console.ReadLine();
                valid = Char.IsLetter(input[0]);
                if (valid)
                {
                    char userInput = input[0];
                    switch (userInput)
                    {
                        case 'l':
                            Console.WriteLine("ID | Name");
                            Console.WriteLine("-----------------");
                            foreach (var l in locations)
                            {
                                Console.WriteLine($"{l.ID}  | {l.Name}");
                            }
                            valid = false;
                            break;
                        case 'b':
                            // go up a level in the menu
                            return;
                        case 'q':
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
                else if (char.IsDigit(input[0]))
                {
                    int i = input[0] - '0';
                    valid = locations.Any(c => c.ID == i);
                    if (valid)
                    {
                        var loc = locations.Find(l => l.ID == i);
                        var order = PlaceOrderChooseProducts(hs, loc, customerID);
                        if(order == null)
                        {
                            valid = false;
                            break;
                        }
                        else
                        {
                            ReviewAndSubmitOrder(hs, order, loc);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid location ID number. Please enter another number.");
                    }
                }
            } while (!valid);
        }

        /// <summary>
        /// Allows the user to choose the products they will order now that the customer and location for the order have been chosen.
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="loc"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        static Domain.Order PlaceOrderChooseProducts(HelperService hs, Domain.Location loc, int customerID)
        {
            Domain.Order order = new Domain.Order();
            order.LocationID = loc.ID;
            order.CustomerID = customerID;
            string input = "";
            bool valid = false;
            do
            {
                Console.WriteLine("\nEnter the ID of a product to add to the order:");
                Console.WriteLine("l : View list of products at this location");
                Console.WriteLine("f : Review and finish the order");
                Console.WriteLine("b : Go back a menu");
                Console.WriteLine("q : Quit out of the program");
                input = "";
                input = Console.ReadLine();
                valid = Char.IsLetter(input[0]);
                if (valid)
                {
                    char userInput = input[0];
                    switch (userInput)
                    {
                        case 'l':
                            Console.WriteLine("{0, -3} | {1, -50} {2, -15} {3, -10}", "ID", "Name", "Price per unit", "Amount in stock");
                            Console.WriteLine("-------------------------------------------------------------------------------------");
                            foreach (var kv in loc.Inventory)
                            {
                                Console.WriteLine($"{kv.Key.ID,-3} | {kv.Key.Name, -50} {kv.Key.Price, -15} {kv.Value, -10}");
                            }
                            valid = false;
                            break;
                        case 'f':
                            return order;
                        case 'b':
                            // go up a level in the menu
                            return null;
                        case 'q':
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
                else if (input.All(Char.IsDigit))
                {
                    int i = Int32.Parse(input);
                    valid = loc.Inventory.Any(kv => kv.Key.ID == i);
                    if (valid)
                    {
                        Console.WriteLine("\nEnter the amount of the product to order:");
                        Console.WriteLine("b : Go back a menu");
                        Console.WriteLine("q : Quit out of the program");
                        string amt = Console.ReadLine();
                        if (Char.IsLetter(amt[0]))
                        {
                            char u = amt[0];
                            switch (u)
                            {
                                case 'b':
                                    valid = false;
                                    break;
                                case 'q':
                                    Environment.Exit(0);
                                    break;
                            }
                        }
                        else if (amt.All(Char.IsDigit))
                        {
                            int amount = Int32.Parse(amt);
                            if (amount <= loc.GetProductAmount(i))
                            {
                                var p = loc.Inventory.Keys.Where(x => x.ID == i).First();
                                Console.WriteLine($"\nSetting an amount for {p.Name}\n");
                                order.SetItemAmount(p, amount);
                                loc.SetProductAmount(p, loc.GetProductAmount(p) - amount);
                                valid = false;
                            }
                            else
                            {
                                Console.WriteLine("Not enough stock of that product to fulfill that order. Please enter a new amount.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID number. Please enter another number.");
                    }
                }
            } while (!valid);
            return null;
        }

        /// <summary>
        /// Allows the user to review their order and submit it, or to quit the program.
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="order"></param>
        /// <param name="loc"></param>
        /// <returns></returns>
        static bool ReviewAndSubmitOrder(HelperService hs, Domain.Order order, Domain.Location loc)
        {
            Console.WriteLine("Here is a summary of the order:");
            Console.WriteLine($"CustomerID: {order.CustomerID}\tLocationID: {order.LocationID}");
            Console.WriteLine("{0, -3} | {1, -50} {2, -15} {3, -10}", "ID", "Name", "Price per unit", "Amount of units");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            foreach (var kv in order.Items)
            {
                Console.WriteLine($"{kv.Key.ID,-3} | {kv.Key.Name,-50} {kv.Key.Price,-15} {kv.Value,-10}");
            }
            Console.WriteLine($"\nTotal Price: ${order.Total}");
            Console.WriteLine("\nEnter 'c' to continue and submit the order, or 'q' to quit:");
            string input = "";
            do
            {
                input = Console.ReadLine();
                if (input[0] == 'c')
                {
                    order.Time = DateTimeOffset.Now;
                    hs.AddOrder(order);
                    hs.UpdateLocation(loc);
                    Console.WriteLine("\nThank you for placing your order!");
                    return true;
                }
                else if (input[0] == 'q')
                {
                    Environment.Exit(0);
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please type 'c' to continue and submit the order, or 'q' to quit:");
                }
            } while (true);
        }
    }
}
