using Entities.Abstract;

namespace Entities.Concrete;

public class Discount:IEntity
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public double Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MinCost { get; set; }
    public string? Description { get; set; }
}