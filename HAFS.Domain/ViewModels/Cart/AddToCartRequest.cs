namespace Domain.ViewModels.Cart;

public class AddToCartRequest
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public string? ImagePath { get; set; }
    public int Quantity { get; set; }
}