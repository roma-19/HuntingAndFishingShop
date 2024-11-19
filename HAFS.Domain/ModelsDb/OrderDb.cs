using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ModelsDb;

[Table("Orders")]
public class OrderDb
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Column("product_id")]
    public Guid ProductId { get; set; }
    
    [Column("name")]
    public string? Name { get; set; }
    
    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
}