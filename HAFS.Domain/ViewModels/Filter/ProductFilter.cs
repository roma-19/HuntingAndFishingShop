namespace Domain.ViewModels.Filter;

public class ProductFilter
{
    public Guid CategoryId { get; set; }
    
    public decimal? MinPrice { get; set; }
    
    public decimal? MaxPrice { get; set; }
}