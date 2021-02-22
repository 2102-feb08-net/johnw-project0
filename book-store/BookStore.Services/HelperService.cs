using BookStore.Domain;
using System.Collections.Generic;

namespace BookStore.Services
{
    public class HelperService
    {
        private IRepository _repo;

        public HelperService(IRepository repository)
        {
            _repo = repository;
        }

        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public Customer GetCustomerByID(int id)
        {
            return _repo.GetCustomerByID(id);
        }

        public List<Customer> GetCustomerByName(string first, string last)
        {
            return _repo.GetCustomerByName(first, last);
        }

        public void AddCustomer(string firstName, string lastName, int defaultLocationId)
        {
            _repo.AddCustomer(new Customer(firstName, lastName, defaultLocationId));
        }

        public void AddCustomer(Customer c)
        {
            _repo.AddCustomer(c);
        }

        public void UpdateCustomer(Customer c)
        {
            _repo.UpdateCustomer(c);
        }

        public void DeleteCustomer(Customer c)
        {
            _repo.DeleteCustomer(c);
        }

        // Products
        public List<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
        }

        public Product GetProductByID(int id)
        {
            return _repo.GetProductByID(id);
        }

        public List<Product> GetProductByName(string name)
        {
            return _repo.GetProductByName(name);
        }

        public void AddProduct(Product p)
        {
            _repo.AddProduct(p);
        }

        public void UpdateProduct(Product p)
        {
            _repo.UpdateProduct(p);
        }

        public void DeleteProduct(Product p)
        {
            _repo.DeleteProduct(p);
        }

        // Location
        public List<Location> GetAllLocations()
        {
            return _repo.GetAllLocations();
        }

        public Location GetLocationByID(int id)
        {
            return _repo.GetLocationByID(id);
        }

        public Location GetLocationByName(string name)
        {
            return _repo.GetLocationByName(name);
        }

        public void AddLocation(Location l)
        {
            _repo.AddLocation(l);
        }

        public void UpdateLocation(Location l)
        {
            _repo.UpdateLocation(l);
        }

        public void DeleteLocation(Location l)
        {
            _repo.DeleteLocation(l);
        }

        // Orders
        public List<Order> GetAllOrders()
        {
            return _repo.GetAllOrders();
        }

        public Order GetOrderByID(int id)
        {
            return _repo.GetOrderByID(id);
        }

        public List<Order> GetCustomerOrderHistory(int customerID)
        {
            return _repo.GetOrdersByCustomerID(customerID);
        }

        public List<Order> GetLocationOrderHistory(int locationID)
        {
            return _repo.GetOrdersByLocationID(locationID);
        }

        public void AddOrder(Order o)
        {
            _repo.AddOrder(o);
        }
    }
}
