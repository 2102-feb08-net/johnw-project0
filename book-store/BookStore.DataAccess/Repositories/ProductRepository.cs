using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.DataAccess
{
    public class ProductRepository<T> : IRepository<T> where T : Domain.Product
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

        public Domain.Product GetById(int id)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            var c = _context.Set<Product>().Find(id);
            var x = new Domain.Product(c.Name, (decimal)c.Price);
            return x;
        }

        public Domain.Product GetByName(string n)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            var c = _context.Set<Product>().Where(x => n == x.Name).FirstOrDefault();
            return new Domain.Product(c.Name, (decimal)c.Price);
        }

        public IEnumerable<Domain.Product> List()
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            var x = _context.Set<Product>().AsEnumerable();
            List<Domain.Product> list = new List<Domain.Product>();
            foreach (var i in x)
            {
                var p = new Domain.Product(i.Name, (decimal)i.Price);
                list.Add(p);
            }
            return list;
        }

        public void Insert(Domain.Product p)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            Product entity = new Product() {Name = p.Name, Price = p.Price};
            _context.Set<Product>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(Domain.Product p)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            var entity = _context.Products.SingleOrDefault(x => x.Id == p.ID);
            if (entity != null)
            {
                entity.Name = p.Name;
                entity.Price = p.Price;
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Delete(Domain.Product p)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            ConnectToDB(logStream);
            using var _context = new bookstoredbContext(options);
            var entity = _context.Products.SingleOrDefault(x => x.Id == p.ID);
            if (entity != null)
            {
                _context.Set<Product>().Remove(entity);
                _context.SaveChanges();
            }

        }
    }
}
