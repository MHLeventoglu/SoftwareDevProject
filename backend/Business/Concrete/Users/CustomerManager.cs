using Business.Abstract.Users;
using Core.Utilities.Results;
using DataAccess.Abstract.Users;
using Entities.Concrete.Users;
using System.Collections.Generic;

namespace Business.Concrete.Users
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer entity)
        {
            _customerDal.Add(entity);
            return new SuccessResult("Customer added successfully.");
        }

        public IResult Delete(Customer entity)
        {
            if (entity == null)
                return new ErrorResult("Customer not found.");

            _customerDal.Delete(entity);
            return new SuccessResult("Customer deleted successfully.");
        }

        public IResult Update(Customer entity)
        {
            if (entity == null)
                return new ErrorResult("Customer not found.");

            _customerDal.Update(entity);
            return new SuccessResult("Customer updated successfully.");
        }

        public IDataResult<List<Customer>> GetAll()
        {
            var customers = _customerDal.GetAll();
            return new SuccessDataResult<List<Customer>>(customers);
        }

        public IDataResult<Customer> GetById(int id)
        {
            var customer = _customerDal.Get(c => c.Id == id);
            if (customer == null)
                return new ErrorDataResult<Customer>("Customer not found.");

            return new SuccessDataResult<Customer>(customer);
        }
    }
}
