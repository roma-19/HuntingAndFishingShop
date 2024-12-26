namespace Domain.ViewModels.Cart;

public class CartViewModel
{
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public string? ImagePath { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}