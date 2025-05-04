using System.Linq.Expressions;
using DataAccess.Abstract.Users;
using Entities.Concrete.Users;

namespace DataAccess.Concrete.InMemory;

public class InMemoryCustomerDal : ICustomerDal
{
    readonly List<Customer> _customers;

    public InMemoryCustomerDal()
    {
        _customers = new List<Customer>
        {
            new Customer { Id = 1, FirstName = "John", Surname = "Doe", Email = "john.doe@example.com" },
            new Customer { Id = 2, FirstName = "Jane", Surname = "Smith", Email = "jane.smith@example.com" },
            new Customer { Id = 3, FirstName = "Alice", Surname = "Johnson", Email = "alice.johnson@example.com" }
        };
    }

    public void Add(Customer customer)
    {
        _customers.Add(customer);
    }

    public void Delete(Customer customer)
    {
        Customer? customerToDelete = _customers.SingleOrDefault(c => c.Id == customer.Id);
        if (customerToDelete != null)
        {
            _customers.Remove(customerToDelete);
        }
    }

    public Customer Get(Expression<Func<Customer, bool>>? filter)
    {
        Func<Customer, bool> compiledFilter = filter.Compile();
        return _customers.SingleOrDefault(compiledFilter);
    }

    public List<Customer> GetAll(Expression<Func<Customer, bool>>? filter = null)
    {
        return filter == null ? _customers : _customers.Where(filter.Compile()).ToList();
    }

    public void Update(Customer customer)
    {
        Customer? customerToUpdate = _customers.SingleOrDefault(c => c.Id == customer.Id);
        if (customerToUpdate != null)
        {
            customerToUpdate.FirstName = customer.FirstName;
            customerToUpdate.Surname = customer.Surname;
            customerToUpdate.Email = customer.Email;
        }
    }
}