using Entities.Abstract;

namespace Entities.Concrete.Users;

public class Staff : User, IEntity
{
    public int TypeId { get; set; }
}