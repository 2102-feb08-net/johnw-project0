using System;

namespace BookStore.Domain
{
    public class Customer
    {
        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        
        public Customer(string firstName, string lastName, int defaultStoreId)
        {
            FirstName = firstName;
            LastName = lastName;
            DefaultStoreID = defaultStoreId;
        }

        public int ID {get;}
        public string FirstName {get;}
        public string LastName {get;}
        public int DefaultStoreID {get; set;}
    }
}
