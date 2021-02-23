using BookStore.Domain;
using System.Collections.Generic;

namespace BookStore.Services
{
    /// <summary>
    /// A class for interfacing between the interface application and the data access library.
    /// </summary>
    public class HelperService
    {
        /// <summary>
        /// The repository that contains all of the data access methods.
        /// </summary>
        private IRepository _repo;

        public HelperService(IRepository repository)
        {
            _repo = repository;
        }

        /// <summary>
        /// Returns a list of all the Customers returned by the repo.
        /// </summary>
        /// <returns> List<Customer> toReturn </returns>
        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        /// <summary>
        /// Returns the Customer that the repo returned matching the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomerByID(int id)
        {
            return _repo.GetCustomerByID(id);
        }

        /// <summary>
        /// Returns the list of Customers that the repo returned matching the given first and last names.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns> List<Customer> toReturn </returns>
        public List<Customer> GetCustomerByName(string first, string last)
        {
            return _repo.GetCustomerByName(first, last);
        }

        /// <summary>
        /// Constructs a new Customer based on the given data to then add to the repo.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="defaultLocationId"></param>
        public void AddCustomer(string firstName, string lastName, int defaultLocationId)
        {
            _repo.AddCustomer(new Customer(firstName, lastName, defaultLocationId));
        }

        /// <summary>
        /// Adds the given Customer to the repo.
        /// </summary>
        /// <param name="c"></param>
        public void AddCustomer(Customer c)
        {
            _repo.AddCustomer(c);
        }

        /// <summary>
        /// Updates the given Customer in the repo.
        /// </summary>
        /// <param name="c"></param>
        public void UpdateCustomer(Customer c)
        {
            _repo.UpdateCustomer(c);
        }


        /// <summary>
        /// Deletes the given Customer from the repo.
        /// </summary>
        /// <param name="c"></param>
        public void DeleteCustomer(Customer c)
        {
            _repo.DeleteCustomer(c);
        }

        // Products

        /// <summary>
        /// Returns the list of all Products that are returned by the repo.
        /// </summary>
        /// <returns> List<Product> toReturn </returns>
        public List<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
        }

        /// <summary>
        /// Returns the Product returned by the repo that matches the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Product p </returns>
        public Product GetProductByID(int id)
        {
            return _repo.GetProductByID(id);
        }

        /// <summary>
        /// Returns the list of Products returned by the repo that match the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns> List<Product> toReturn </returns>
        public List<Product> GetProductByName(string name)
        {
            return _repo.GetProductByName(name);
        }

        /// <summary>
        /// Adds the given Product to the repo.
        /// </summary>
        /// <param name="p"></param>
        public void AddProduct(Product p)
        {
            _repo.AddProduct(p);
        }

        /// <summary>
        /// Updates the given Product in the repo.
        /// </summary>
        /// <param name="p"></param>
        public void UpdateProduct(Product p)
        {
            _repo.UpdateProduct(p);
        }

        /// <summary>
        /// Deletes the given Product from the repo.
        /// </summary>
        /// <param name="p"></param>
        public void DeleteProduct(Product p)
        {
            _repo.DeleteProduct(p);
        }

        // Location

        /// <summary>
        /// Returns the list of all Locations returned by the repo.
        /// </summary>
        /// <returns> List<Location> toReturn </returns>
        public List<Location> GetAllLocations()
        {
            return _repo.GetAllLocations();
        }

        /// <summary>
        /// Returns the Location that the repo returned that matches the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Location loc </returns>
        public Location GetLocationByID(int id)
        {
            return _repo.GetLocationByID(id);
        }

        /// <summary>
        /// Returns the Location that the repo returned that matches the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns> Location loc </returns>
        public Location GetLocationByName(string name)
        {
            return _repo.GetLocationByName(name);
        }

        /// <summary>
        /// Adds the given Location to the repo.
        /// </summary>
        /// <param name="l"></param>
        public void AddLocation(Location l)
        {
            _repo.AddLocation(l);
        }

        /// <summary>
        /// Updates the given Location in the repo.
        /// </summary>
        /// <param name="l"></param>
        public void UpdateLocation(Location l)
        {
            _repo.UpdateLocation(l);
        }

        /// <summary>
        /// Deletes the given Location from the repo.
        /// </summary>
        /// <param name="l"></param>
        public void DeleteLocation(Location l)
        {
            _repo.DeleteLocation(l);
        }

        // Orders

        /// <summary>
        /// Returns the list of all Orders that the repo returned.
        /// </summary>
        /// <returns> List<Order> toReturn </returns>
        public List<Order> GetAllOrders()
        {
            return _repo.GetAllOrders();
        }

        /// <summary>
        /// Returns the Order that the repo returned matching the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Order o </returns>
        public Order GetOrderByID(int id)
        {
            return _repo.GetOrderByID(id);
        }

        /// <summary>
        /// Returns the list that the repo returned of all Orders made by the Customer matching the given ID.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns> List<Order> toReturn </returns>
        public List<Order> GetCustomerOrderHistory(int customerID)
        {
            return _repo.GetOrdersByCustomerID(customerID);
        }

        /// <summary>
        /// Returns the list that the repo returned of all Orders made at the Location matching the given ID.
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public List<Order> GetLocationOrderHistory(int locationID)
        {
            return _repo.GetOrdersByLocationID(locationID);
        }

        /// <summary>
        /// Adds an Order to the repo.
        /// </summary>
        /// <param name="o"></param>
        public void AddOrder(Order o)
        {
            _repo.AddOrder(o);
        }
    }
}
