namespace Entities.Concrete.Users;
using Core.Entities;

public class StaffType : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}