using System.Linq.Expressions;
using Core.Entities;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IBaseService<T> where T : IEntity
    {
        // Bu interface Bütün servicelerin ortak olarak kullanacağı metotları tanımlıyor 
        IResult Add(T entity);
        IResult Delete(T entity);
        IResult Update(T entity);
        IDataResult<List<T>> GetAll(); 
        IDataResult<T> GetById(int id);
    }
}