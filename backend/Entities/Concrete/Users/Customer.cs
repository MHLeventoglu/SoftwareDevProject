using Core.Entities;

namespace Entities.Concrete.Users;

public class Customer : User, IEntity
{
    public float Balance { get; set; }
}