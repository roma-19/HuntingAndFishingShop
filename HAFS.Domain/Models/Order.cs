namespace Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid ProductId { get; set; }
    
    public string? Name { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
}