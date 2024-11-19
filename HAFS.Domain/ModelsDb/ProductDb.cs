using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ModelsDb;

[Table("Products")]
public class ProductDb
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("category_id")]
    public Guid CategoryId { get; set; }
    
    [Column("name")]
    public string? Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("image_path")]
    public string? ImagePath { get; set; }
}