using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess
{
    public class CustomerRepository<T> : IRepository<T> where T : Domain.Customer
    {
        private readonly bookstoredbContext _context;

        public CustomerRepository()
        {
            var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            string connString = File.ReadAllText("C:/revature/bookstore-db-connection-string.txt");
            DbContextOptions<bookstoredbContext> options = new DbContextOptionsBuilder<bookstoredbContext>()
                .UseSqlServer(connString)
                .LogTo(logStream.WriteLine, minimumLevel: LogLevel.Information)
                .Options;
            _context = new bookstoredbContext(options);
        }

        public CustomerRepository(bookstoredbContext ctx)
        {
            _context = ctx;
        }

        public Domain.Customer GetById(int id)
        {
            var c = _context.Set<Customer>().Find(id);
            var x = new Domain.Customer(c.FirstName, c.LastName) { ID = c.Id, FirstName = c.FirstName, LastName = c.LastName, DefaultLocationID = (int)c.DefaultLocationId };
            return x;
        }

        public Domain.Customer GetByFullName(string n)
        {
            var c = _context.Set<Customer>().Where(x => n == (x.FirstName + " " + x.LastName)).FirstOrDefault();
            return new Domain.Customer(c.Id, c.FirstName, c.LastName, (int)c.DefaultLocationId);
        }

        public IEnumerable<Domain.Customer> List()
        {
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
            Customer entity = new Customer() { FirstName = c.FirstName, LastName = c.LastName, DefaultLocationId = c.DefaultLocationID };
            _context.Set<Customer>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(Domain.Customer c)
        {
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
            var entity = _context.Customers.SingleOrDefault(x => x.Id == c.ID);
            if (entity != null)
            {
                _context.Set<Customer>().Remove(entity);
                _context.SaveChanges();
            }
            
        }
    }
}
