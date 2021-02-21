using System;
using System.Collections.Generic;

namespace BookStore.Domain
{
    public class Store
    {
        private List<Customer> CustomerList {get;}
        private Dictionary<int, List<Order>> locationOrders;
        private Dictionary<int, List<Order>> customerOrders;

        public Store()
        {
            locationOrders = new Dictionary<int, List<Order>>();
            customerOrders = new Dictionary<int, List<Order>>();
        }
        // Add a new customer
        public void AddCustomer(string firstName, string lastName)
        {
            Customer c = new Customer(firstName, lastName);
        }

        // Search customers by name
        public Customer GetCustomerByName(string query)
        {
            foreach (Customer c in CustomerList)
            {
                if (c.FirstName.Contains(query) || c.LastName.Contains(query))
                {
                    return c;
                }
            }
            return null;
        }

        // Place orders to store locations for customers
        public void PlaceOrder(Order o)
        {

        }

        // Display details of an order
        
        // Display all order history of a store location
        public List<Order> GetLocationOrderHistory(int locationID)
        {
            return locationOrders[locationID];
        }

        // Display all order history of a customer
        public List<Order> GetCustomerOrderHistory(int customerID)
        {
            return customerOrders[customerID];
        }
    }
}