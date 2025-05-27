using Business.Abstract.Users;
using Core.Utilities.Results;
using DataAccess.Abstract.Users;
using Entities.Concrete.Users;
using System.Collections.Generic;

namespace Business.Concrete.Users
{
    public class StaffTypeManager : IStaffTypeService
    {
        private readonly IStaffTypeDal _staffTypeDal;

        public StaffTypeManager(IStaffTypeDal staffTypeDal)
        {
            _staffTypeDal = staffTypeDal;
        }

        public IResult Add(StaffType entity)
        {
            _staffTypeDal.Add(entity);
            return new SuccessResult("StaffType added successfully.");
        }

        public IResult Delete(StaffType entity)
        {
            if (entity == null)
                return new ErrorResult("StaffType not found.");

            _staffTypeDal.Delete(entity);
            return new SuccessResult("StaffType deleted successfully.");
        }

        public IResult Update(StaffType entity)
        {
            if (entity == null)
                return new ErrorResult("StaffType not found.");

            _staffTypeDal.Update(entity);
            return new SuccessResult("StaffType updated successfully.");
        }

        public IDataResult<List<StaffType>> GetAll()
        {
            var staffTypes = _staffTypeDal.GetAll();
            return new SuccessDataResult<List<StaffType>>(staffTypes);
        }

        public IDataResult<StaffType> GetById(int id)
        {
            var staffType = _staffTypeDal.Get(st => st.Id == id);
            if (staffType == null)
                return new ErrorDataResult<StaffType>("StaffType not found.");

            return new SuccessDataResult<StaffType>(staffType);
        }
    }
}
