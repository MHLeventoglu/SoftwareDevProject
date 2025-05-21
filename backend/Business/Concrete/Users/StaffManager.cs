using Business.Abstract.Users;
using Core.Utilities.Results;
using DataAccess.Abstract.Users;
using Entities.Concrete.Users;
using System.Collections.Generic;

namespace Business.Concrete.Users
{
    public class StaffManager : IStaffService
    {
        private readonly IStaffDal _staffDal;

        public StaffManager(IStaffDal staffDal)
        {
            _staffDal = staffDal;
        }

        public IResult Add(Staff entity)
        {
            _staffDal.Add(entity);
            return new SuccessResult("Staff added successfully.");
        }

        public IResult Delete(Staff entity)
        {
            if (entity == null)
                return new ErrorResult("Staff not found.");

            _staffDal.Delete(entity);
            return new SuccessResult("Staff deleted successfully.");
        }

        public IResult Update(Staff entity)
        {
            if (entity == null)
                return new ErrorResult("Staff not found.");

            _staffDal.Update(entity);
            return new SuccessResult("Staff updated successfully.");
        }

        public IDataResult<List<Staff>> GetAll()
        {
            var staffs = _staffDal.GetAll();
            return new SuccessDataResult<List<Staff>>(staffs);
        }

        public IDataResult<Staff> GetById(int id)
        {
            var staff = _staffDal.Get(s => s.Id == id);
            if (staff == null)
                return new ErrorDataResult<Staff>("Staff not found.");

            return new SuccessDataResult<Staff>(staff);
        }
    }
}
