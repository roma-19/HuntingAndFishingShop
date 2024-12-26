using Domain.ModelsDb;

namespace Domain.Models;

public class CartItem
{
    public Guid Id { get; set; }
    
    public Guid ProductId { get; set; }
    
    public string? ImagePath { get; set; }
    
    public decimal Price { get; set; }
    
    public int Quantity { get; set; }
    
    public string UserEmail { get; set; }
    
    public ProductDb Product { get; set; }
}