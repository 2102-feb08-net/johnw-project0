using BookStore.DataAccess;
using System;
using System.Collections.Generic;

namespace BookStore.Services
{
    public class CustomerService
    {
        private CustomerRepository<Domain.Customer> _repo;

        public CustomerService()
        {
            _repo = new CustomerRepository<Domain.Customer>();
        }

        public List<Domain.Customer> GetAllCustomers()
        {
            return (List<Domain.Customer>)_repo.List();
        }

        public Domain.Customer GetCustomerByFullName(string name)
        {
            return _repo.GetByFullName(name);
        }

        public void AddCustomer(string firstName, string lastName, int defaultLocationID)
        {
            Domain.Customer c = new Domain.Customer(firstName, lastName) { DefaultLocationID = defaultLocationID };
            _repo.Insert(c);
        }

        public void AddCustomer(Domain.Customer c)
        {
            _repo.Insert(c);
        }

        public void UpdateCustomer(Domain.Customer c)
        {
            _repo.Update(c);
        }

        public void DeleteCustomer(Domain.Customer c)
        {
            _repo.Delete(c);
        }
    }
}
