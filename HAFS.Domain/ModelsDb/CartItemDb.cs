using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace Domain.ModelsDb;

[Table("CartItems")]
public class CartItemDb
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("productId")]
    public Guid ProductId { get; set; }
    
    [Column("imagePath")]
    public string? ImagePath { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("userEmail")]
    public string UserEmail { get; set; }
    
    public ProductDb Product { get; set; }
}