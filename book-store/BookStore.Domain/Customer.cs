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

        public Customer(string firstName, string lastName, int defaultLocationId)
        {
            FirstName = firstName;
            LastName = lastName;
            DefaultLocationID = defaultLocationId;
        }

        public int ID {get;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int DefaultLocationID {get; set;}
    }
}
