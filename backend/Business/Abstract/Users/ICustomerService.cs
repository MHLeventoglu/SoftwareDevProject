using Entities.Concrete.Users;

namespace Business.Abstract.Users
{
    public interface ICustomerService : IBaseService<Customer>
    {
        // Customer'a özel metotlar varsa buraya ekle
    }
}