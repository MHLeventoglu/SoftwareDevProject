using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete.Users;

public class Customer : User, IEntity
{
    public float Balance { get; set; }
}