using System;

namespace BookStore.Domain
{
    public class Product
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public Product(int id, string name, double price)
        {
            ID = id;
            Name = name;
            Price = price;
        }

        public int ID {get; set;}
        public string Name {get; set;}
        public double Price {get; set;}
    }
}