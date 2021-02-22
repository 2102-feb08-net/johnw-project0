using System.Collections.Generic;

namespace BookStore.Domain
{
    public interface IRepository
    {
        // CRUD Customer
        public List<Customer> GetAllCustomers();
        public Customer GetCustomerByID(int id);
        public List<Customer> GetCustomerByName(string first, string last);
        public void AddCustomer(Customer c);
        public void UpdateCustomer(Customer c);
        public void DeleteCustomer(Customer c);

        // CRUD Product
        public List<Product> GetAllProducts();
        public Product GetProductByID(int id);
        public List<Product> GetProductByName(string name);
        public void AddProduct(Product p);
        public void UpdateProduct(Product p);
        public void DeleteProduct(Product p);

        // CRUD Location
        public List<Location> GetAllLocations();
        public Location GetLocationByID(int id);
        public Location GetLocationByName(string name);
        public void AddLocation(Location l);
        public void UpdateLocation(Location l);
        public void DeleteLocation(Location l);

        // CRUD Order
        public List<Order> GetAllOrders();
        public Order GetOrderByID(int id);
        public List<Order> GetOrdersByCustomerID(int customerID);
        public List<Order> GetOrdersByLocationID(int locationID);
        public void AddOrder(Order o);
        public void UpdateOrder(Order o);
        public void DeleteOrder(Order o);

    }
}
