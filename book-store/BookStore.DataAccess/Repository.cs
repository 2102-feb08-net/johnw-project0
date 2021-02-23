using BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.DataAccess
{
    public class Repository : IRepository
    {
        /// <summary>
        /// Generates the context for accessing the database based on options that are given.
        /// </summary>
        /// <param name="logStream"></param>
        /// <returns> bookstoredbContext _context </returns>
        private bookstoredbContext GenerateDBContext(StreamWriter logStream)
        {
            string connString = File.ReadAllText("C:/revature/bkdb.txt");
            DbContextOptions<bookstoredbContext> options = new DbContextOptionsBuilder<bookstoredbContext>()
                .UseSqlServer(connString)
                .LogTo(logStream.WriteLine, minimumLevel: LogLevel.Information)
                .Options;

            return new bookstoredbContext(options);
        }

        // CRUD Customer

        /// <summary>
        /// Returns the list of all Customers in the database.
        /// </summary>
        /// <returns> List<Customer> list </returns>
        public List<Domain.Customer> GetAllCustomers()
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var x = _context.Set<Customer>().AsEnumerable();
            List<Domain.Customer> list = new List<Domain.Customer>();
            
            foreach (var i in x)
            {
                var c = new Domain.Customer(i.FirstName, i.LastName, (int)i.DefaultLocationId) { ID = i.Id };
                list.Add(c);
            }
            
            return list;
        }

        /// <summary>
        /// Returns a specific Customer with the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Customer x </returns>
        public Domain.Customer GetCustomerByID(int id)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var c = _context.Set<Customer>().Find(id);
            var x = new Domain.Customer(c.FirstName, c.LastName) { ID = c.Id, FirstName = c.FirstName, LastName = c.LastName, DefaultLocationID = (int)c.DefaultLocationId };
            
            return x;
        }

        /// <summary>
        /// Returns a list of Customers that have the given first and last name.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns> List<Customer> toReturn </returns>
        public List<Domain.Customer> GetCustomerByName(string first, string last)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var customers = _context.Set<Customer>().Where(x => x.FirstName == first && x.LastName == last).ToList();
            List<Domain.Customer> toReturn = new List<Domain.Customer>();
            
            foreach (var c in customers)
            {
                var x = new Domain.Customer(c.Id, c.FirstName, c.LastName, (int)c.DefaultLocationId);
                toReturn.Add(x);
            }
            
            return toReturn;
        }

        /// <summary>
        /// Adds the given Customer as a new Customer to the database.
        /// </summary>
        /// <param name="c"></param>
        public void AddCustomer(Domain.Customer c)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            Customer entity = new Customer() { FirstName = c.FirstName, LastName = c.LastName, DefaultLocationId = c.DefaultLocationID };
            _context.Set<Customer>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates the given Customer in the database.
        /// </summary>
        /// <param name="c"></param>
        public void UpdateCustomer(Domain.Customer c)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var entity = _context.Customers.SingleOrDefault(x => x.Id == c.ID);
            if (entity != null)
            {
                entity.FirstName = c.FirstName;
                entity.LastName = c.LastName;
                entity.DefaultLocationId = c.DefaultLocationID;
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the given Customer from the database.
        /// </summary>
        /// <param name="c"></param>
        public void DeleteCustomer(Domain.Customer c)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var entity = _context.Customers.SingleOrDefault(x => x.Id == c.ID);
            if (entity != null)
            {
                _context.Set<Customer>().Remove(entity);
                _context.SaveChanges();
            }
        }

        // CRUD Product

        /// <summary>
        /// Returns the list of all Products in the database.
        /// </summary>
        /// <returns> List<Product> list </returns>
        public List<Domain.Product> GetAllProducts()
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var x = _context.Set<Product>().AsEnumerable();
            List<Domain.Product> list = new List<Domain.Product>();

            foreach (var i in x)
            {
                var p = new Domain.Product(i.Name, (decimal)i.Price) { ID = i.Id };
                list.Add(p);
            }
            
            return list;
        }

        /// <summary>
        /// Returns the specific Product that has the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Product x </returns>
        public Domain.Product GetProductByID(int id)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var c = _context.Set<Product>().Find(id);
            var x = new Domain.Product(c.Name, (decimal)c.Price);
            
            return x;
        }

        /// <summary>
        /// Returns the list of products that match the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns> List<Product> toReturn </returns>
        public List<Domain.Product> GetProductByName(string name)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var products = _context.Set<Product>().Where(x => name == x.Name).ToList();
            List<Domain.Product> toReturn = new List<Domain.Product>();
            
            foreach(var p in products)
            {
                var x = new Domain.Product(p.Name, (decimal)p.Price) { ID = p.Id };
                toReturn.Add(x);
            }
            
            return toReturn;
        }

        /// <summary>
        /// Adds the given Product to the database.
        /// </summary>
        /// <param name="p"></param>
        public void AddProduct(Domain.Product p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the given Product in the database.
        /// </summary>
        /// <param name="p"></param>
        public void UpdateProduct(Domain.Product p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the given Product from the database.
        /// </summary>
        /// <param name="p"></param>
        public void DeleteProduct(Domain.Product p)
        {
            throw new NotImplementedException();
        }

        // CRUD Location

        /// <summary>
        /// Returns the list of all Locations from the database.
        /// </summary>
        /// <returns> List<Location> toReturn </returns>
        public List<Domain.Location> GetAllLocations()
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var dbLocations = _context.Set<Location>().ToList();
            List<Domain.Location> toReturn = new List<Domain.Location>();

            foreach(var l in dbLocations)
            {
                var n = new Domain.Location(l.Id) { Name = l.Name };
                var inventories = _context.Set<Inventory>().Where(i => i.LocationId == l.Id).ToList();
                foreach (var i in inventories)
                {
                    var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                    var domProduct = new Domain.Product(dbProduct.Id, dbProduct.Name, (decimal)dbProduct.Price);
                    n.SetProductAmount(domProduct, i.Amount);
                }
                toReturn.Add(n);
            }

            return toReturn;
        }

        /// <summary>
        /// Returns the specific Location matching the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Location loc </returns>
        public Domain.Location GetLocationByID(int id)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var l = _context.Set<Location>().Find(id);
            Domain.Location loc = new Domain.Location(id) { Name = l.Name };

            var inventories = _context.Set<Inventory>().Where(i => i.LocationId == id).ToList();
            foreach (var i in inventories)
            {
                var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                var domProduct = new Domain.Product(dbProduct.Id, dbProduct.Name, (decimal)dbProduct.Price);
                loc.SetProductAmount(domProduct, i.Amount);
            }

            return loc;
        }

        /// <summary>
        /// Returns the specific Location matching the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns> Location loc </returns>
        public Domain.Location GetLocationByName(string name)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var l = _context.Set<Location>().Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            Domain.Location loc = new Domain.Location(l.Id) { Name = l.Name };

            var inventories = _context.Set<Inventory>().Where(i => i.LocationId == loc.ID).ToList();
            foreach (var i in inventories)
            {
                var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                var domProduct = new Domain.Product(dbProduct.Id, dbProduct.Name, (decimal)dbProduct.Price);
                loc.SetProductAmount(domProduct, i.Amount);
            }

            return loc;
        }

        /// <summary>
        /// Adds the given Location to the database.
        /// </summary>
        /// <param name="l"></param>
        public void AddLocation(Domain.Location l)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the given Location in the database.
        /// </summary>
        /// <param name="l"></param>
        public void UpdateLocation(Domain.Location l)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var entity = _context.Locations.SingleOrDefault(x => x.Id == l.ID);
            if (entity != null)
            {
                entity.Name = l.Name;
                _context.Entry(entity).State = EntityState.Modified;

                foreach(KeyValuePair<Domain.Product, int> kv in l.Inventory)
                {
                    var i = _context.Find<Inventory>(l.ID, kv.Key.ID);
                    if (i.Amount != kv.Value)
                    {
                        i.Amount = kv.Value;
                        _context.Entry(i).State = EntityState.Modified;
                    }
                }

                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the given Location from the database.
        /// </summary>
        /// <param name="l"></param>
        public void DeleteLocation(Domain.Location l)
        {
            throw new NotImplementedException();
        }

        // CRUD Orders

        /// <summary>
        /// Returns the list of all Orders in the database.
        /// </summary>
        /// <returns> List<Order> toReturn </returns>
        public List<Domain.Order> GetAllOrders()
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var dbOrders = _context.Set<Order>().ToList();
            List<Domain.Order> toReturn = new List<Domain.Order>();

            foreach (var o in dbOrders)
            {
                var n = new Domain.Order(o.Id, o.CustomerId, o.LocationId) { Time = (DateTimeOffset)o.Time };
                var lines = _context.Set<OrderLine>().Where(i => i.OrderId == o.Id).ToList();
                foreach (var i in lines)
                {
                    var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                    var domProduct = new Domain.Product(dbProduct.Name, (decimal)dbProduct.Price);
                    n.SetItemAmount(domProduct, i.Amount);
                }
                toReturn.Add(n);
            }

            return toReturn;
        }

        /// <summary>
        /// Returns the specific Order matching the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Order ord </returns>
        public Domain.Order GetOrderByID(int id)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var o = _context.Set<Order>().Find(id);
            var ord = new Domain.Order(o.Id, o.CustomerId, o.LocationId) { Time = (DateTimeOffset)o.Time };

            var items = _context.Set<OrderLine>().Where(i => i.OrderId == id).ToList();
            foreach (var i in items)
            {
                var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                var domProduct = new Domain.Product(dbProduct.Name, (decimal)dbProduct.Price);
                ord.SetItemAmount(domProduct, i.Amount);
            }

            return ord;
        }

        /// <summary>
        /// Returns the list of Orders placed by the Customer with the given ID.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns> List<Order> toReturn </returns>
        public List<Domain.Order> GetOrdersByCustomerID(int customerID)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var dbOrders = _context.Set<Order>().Where(i => i.CustomerId == customerID).ToList();
            List<Domain.Order> toReturn = new List<Domain.Order>();

            foreach (var o in dbOrders)
            {
                var n = new Domain.Order(o.Id, o.CustomerId, o.LocationId) { Time = (DateTimeOffset)o.Time };
                var lines = _context.Set<OrderLine>().Where(i => i.OrderId == o.Id).ToList();
                foreach (var i in lines)
                {
                    var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                    var domProduct = new Domain.Product(dbProduct.Name, (decimal)dbProduct.Price);
                    n.SetItemAmount(domProduct, i.Amount);
                }
                toReturn.Add(n);
            }

            return toReturn;
        }

        /// <summary>
        /// Returns the list of Orders placed at the Location with the given ID.
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns> List<Order> toReturn </returns>
        public List<Domain.Order> GetOrdersByLocationID(int locationID)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var dbOrders = _context.Set<Order>().Where(i => i.LocationId == locationID).ToList();
            List<Domain.Order> toReturn = new List<Domain.Order>();

            foreach (var o in dbOrders)
            {
                var n = new Domain.Order(o.Id, o.CustomerId, o.LocationId) { Time = (DateTimeOffset)o.Time };
                var lines = _context.Set<OrderLine>().Where(i => i.OrderId == o.Id).ToList();
                foreach (var i in lines)
                {
                    var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                    var domProduct = new Domain.Product(dbProduct.Name, (decimal)dbProduct.Price);
                    n.SetItemAmount(domProduct, i.Amount);
                }
                toReturn.Add(n);
            }

            return toReturn;
        }

        /// <summary>
        /// Adds the given Order to the database.
        /// </summary>
        /// <param name="o"></param>
        public void AddOrder(Domain.Order o)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            Order entity = new Order() { CustomerId = o.CustomerID, LocationId = o.LocationID, Time = o.Time.UtcDateTime, TotalPrice = (decimal)o.Total };
            _context.Set<Order>().Add(entity);
            _context.SaveChanges();
            foreach(var kv in o.Items)
            {
                OrderLine ol = new OrderLine() { OrderId = entity.Id, ProductId = kv.Key.ID, Amount = kv.Value };
                _context.Set<OrderLine>().Add(ol);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates the given Order in the database.
        /// </summary>
        /// <param name="o"></param>
        public void UpdateOrder(Domain.Order o)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the given Order from the database.
        /// </summary>
        /// <param name="o"></param>
        public void DeleteOrder(Domain.Order o)
        {
            throw new NotImplementedException();
        }
    }
}
