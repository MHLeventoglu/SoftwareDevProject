using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete.Users;

public class Staff : User,IEntity
{
    public int TypeId { get; set; }
}