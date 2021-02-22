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
        public Domain.Customer GetCustomerByID(int id)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var c = _context.Set<Customer>().Find(id);
            var x = new Domain.Customer(c.FirstName, c.LastName) { ID = c.Id, FirstName = c.FirstName, LastName = c.LastName, DefaultLocationID = (int)c.DefaultLocationId };
            
            return x;
        }
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
        public void AddCustomer(Domain.Customer c)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            Customer entity = new Customer() { FirstName = c.FirstName, LastName = c.LastName, DefaultLocationId = c.DefaultLocationID };
            _context.Set<Customer>().Add(entity);
            _context.SaveChanges();
        }
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
        public Domain.Product GetProductByID(int id)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var c = _context.Set<Product>().Find(id);
            var x = new Domain.Product(c.Name, (decimal)c.Price);
            
            return x;
        }
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
        public void AddProduct(Domain.Product p)
        {
            throw new NotImplementedException();
        }
        public void UpdateProduct(Domain.Product p)
        {
            throw new NotImplementedException();
        }
        public void DeleteProduct(Domain.Product p)
        {
            throw new NotImplementedException();
        }

        // CRUD Location
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
                    var domProduct = new Domain.Product(dbProduct.Name, (decimal)dbProduct.Price);
                    n.SetProductAmount(domProduct, i.Amount);
                }
                toReturn.Add(n);
            }

            return toReturn;
        }
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
                var domProduct = new Domain.Product(dbProduct.Name, (decimal)dbProduct.Price);
                loc.SetProductAmount(domProduct, i.Amount);
            }

            return loc;
        }
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
                var domProduct = new Domain.Product(dbProduct.Name, (decimal)dbProduct.Price);
                loc.SetProductAmount(domProduct, i.Amount);
            }

            return loc;
        }
        public void AddLocation(Domain.Location l)
        {
            throw new NotImplementedException();
        }
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
        public void DeleteLocation(Domain.Location l)
        {
            throw new NotImplementedException();
        }

        // CRUD Orders
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
        public void AddOrder(Domain.Order o)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            Order entity = new Order() { Id = o.ID, CustomerId = o.CustomerID, LocationId = o.LocationID, Time = o.Time.UtcDateTime };
            _context.Set<Order>().Add(entity);
            foreach(var kv in o.Items)
            {
                OrderLine ol = new OrderLine() { OrderId = o.ID, ProductId = kv.Key.ID, Amount = kv.Value };
                _context.Set<OrderLine>().Add(ol);
            }
            _context.SaveChanges();
        }
        public void UpdateOrder(Domain.Order o)
        {
            throw new NotImplementedException();
        }
        public void DeleteOrder(Domain.Order o)
        {
            throw new NotImplementedException();
        }
    }
}
