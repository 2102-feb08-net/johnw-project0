using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.DataAccess
{
    public class CustomerRepository<T> : IRepository<T> where T : Domain.Customer
    {
        private static DbContextOptions<bookstoredbContext> options;

        private void ConnectToDB(StreamWriter logStream)
        {
            string connString = File.ReadAllText("C:/revature/bkdb.txt");
            options = new DbContextOptionsBuilder<bookstoredbContext>()
                .UseSqlServer(connString)
                .LogTo(logStream.WriteLine, minimumLevel: LogLevel.Information)
                .Options;
        }

        public Domain.Customer GetById(int id)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            var c = _context.Set<Customer>().Find(id);
            var x = new Domain.Customer(c.FirstName, c.LastName) { ID = c.Id, FirstName = c.FirstName, LastName = c.LastName, DefaultLocationID = (int)c.DefaultLocationId };
            return x;
        }

        public Domain.Customer GetByFullName(string n)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            var c = _context.Set<Customer>().Where(x => n == (x.FirstName + " " + x.LastName)).FirstOrDefault();
            return new Domain.Customer(c.Id, c.FirstName, c.LastName, (int)c.DefaultLocationId);
        }

        public IEnumerable<Domain.Customer> List()
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            var x = _context.Set<Customer>().AsEnumerable();
            List<Domain.Customer> list = new List<Domain.Customer>();
            foreach (var i in x)
            {
                var c = new Domain.Customer(i.FirstName, i.LastName, (int)i.DefaultLocationId) { ID = i.Id };
                list.Add(c);
            }
            return list;
        }

        public void Insert(Domain.Customer c)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            Customer entity = new Customer() { FirstName = c.FirstName, LastName = c.LastName, DefaultLocationId = c.DefaultLocationID };
            _context.Set<Customer>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(Domain.Customer c)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
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

        public void Delete(Domain.Customer c)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            var entity = _context.Customers.SingleOrDefault(x => x.Id == c.ID);
            if (entity != null)
            {
                _context.Set<Customer>().Remove(entity);
                _context.SaveChanges();
            }
            
        }
    }
}
