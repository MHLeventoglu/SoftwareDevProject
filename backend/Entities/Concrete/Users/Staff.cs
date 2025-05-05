using Core.Entities;

namespace Entities.Concrete.Users;

public class Staff : User,IEntity
{
    public int TypeId { get; set; }
}