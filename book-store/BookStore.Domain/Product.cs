using System;

namespace BookStore.Domain
{
    public class Product
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Product(int id, string name, decimal price)
        {
            ID = id;
            Name = name;
            Price = price;
        }

        public int ID {get; set;}
        public string Name {get; set;}
        public decimal Price {get; set;}
    }
}