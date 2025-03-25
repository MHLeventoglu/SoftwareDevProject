using Entities.Abstract;

namespace Entities.Concrete;

public class Review:IEntity
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Comment { get; set; }
    public int Rating { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
}