using System;

namespace BookStore.Domain
{
    public class Product : IProduct
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name {get;}
        public double Price {get;}
    }
}