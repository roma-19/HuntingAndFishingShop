using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ModelsDb;


[Table("Categories")]
public class CategoryDb
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("name")]
    public string? Name { get; set; }
    
    List<ProductDb> Products { get; set; }
}