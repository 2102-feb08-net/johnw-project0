using System;

namespace BookStore.Domain
{
    public class Customer
    {
        public Customer(string first, string last)
        {
            FirstName = first;
            LastName = last;
        }

        public string FirstName {get;}
        public string LastName {get;}
    }
}
