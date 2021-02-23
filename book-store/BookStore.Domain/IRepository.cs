using System.Collections.Generic;

namespace BookStore.Domain
{
    public interface IRepository
    {
        // CRUD Customer

        /// <summary>
        /// Returns a list of all Customers in the database.
        /// </summary>
        /// <returns> List<Customer> toReturn </returns>
        public List<Customer> GetAllCustomers();

        /// <summary>
        /// Returns the specific Customer matching the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Customer c </returns>
        public Customer GetCustomerByID(int id);

        /// <summary>
        /// Returns the list of Customers matching the given first and last names.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns> List<Customer> toReturn </returns>
        public List<Customer> GetCustomerByName(string first, string last);

        /// <summary>
        /// Adds the given Customer to the database.
        /// </summary>
        /// <param name="c"></param>
        public void AddCustomer(Customer c);

        /// <summary>
        /// Updates the given Customer in the database.
        /// </summary>
        /// <param name="c"></param>
        public void UpdateCustomer(Customer c);

        /// <summary>
        /// Deletes the given Customer from the database.
        /// </summary>
        /// <param name="c"></param>
        public void DeleteCustomer(Customer c);

        // CRUD Product

        /// <summary>
        /// Returns the list of all Products in the database.
        /// </summary>
        /// <returns> List<Product> toReturn </returns>
        public List<Product> GetAllProducts();

        /// <summary>
        /// Returns the specific Product matching the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Product p </returns>
        public Product GetProductByID(int id);

        /// <summary>
        /// Returns the list of Products matching the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns> List<Product> toReturn </returns>
        public List<Product> GetProductByName(string name);

        /// <summary>
        /// Adds the given Product to the database.
        /// </summary>
        /// <param name="p"></param>
        public void AddProduct(Product p);

        /// <summary>
        /// Updates the given Product in the database.
        /// </summary>
        /// <param name="p"></param>
        public void UpdateProduct(Product p);

        /// <summary>
        /// Deletes the given Product from the database.
        /// </summary>
        /// <param name="p"></param>
        public void DeleteProduct(Product p);

        // CRUD Location

        /// <summary>
        /// Returns the list of all Locations from the database.
        /// </summary>
        /// <returns> List<Location> toReturn </returns>
        public List<Location> GetAllLocations();

        /// <summary>
        /// Returns the specific Location matching the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Location loc </returns>
        public Location GetLocationByID(int id);

        /// <summary>
        /// Returns the specific Location matching the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns> Location loc </returns>
        public Location GetLocationByName(string name);

        /// <summary>
        /// Adds the given Location to the database.
        /// </summary>
        /// <param name="l"></param>
        public void AddLocation(Location l);

        /// <summary>
        /// Updates the given Location in the database.
        /// </summary>
        /// <param name="l"></param>
        public void UpdateLocation(Location l);

        /// <summary>
        /// Deletes the given Location in the database.
        /// </summary>
        /// <param name="l"></param>
        public void DeleteLocation(Location l);

        // CRUD Order

        /// <summary>
        /// Returns the list of all Orders from the database.
        /// </summary>
        /// <returns> List<Order> toReturn </returns>
        public List<Order> GetAllOrders();

        /// <summary>
        /// Returns the specific Order matching the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Order o </returns>
        public Order GetOrderByID(int id);

        /// <summary>
        /// Returns the list of Orders placed by the Customer matching the given ID.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns> List<Order> toReturn </returns>
        public List<Order> GetOrdersByCustomerID(int customerID);

        /// <summary>
        /// Returns the list of Orders placed at the Location matching the given ID.
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns> List<Order> toReturn </returns>
        public List<Order> GetOrdersByLocationID(int locationID);

        /// <summary>
        /// Adds the given Order to the database.
        /// </summary>
        /// <param name="o"></param>
        public void AddOrder(Order o);

        /// <summary>
        /// Updates the given Order in the database.
        /// </summary>
        /// <param name="o"></param>
        public void UpdateOrder(Order o);

        /// <summary>
        /// Deletes the given Order from the database.
        /// </summary>
        /// <param name="o"></param>
        public void DeleteOrder(Order o);

    }
}
