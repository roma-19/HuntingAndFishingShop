using Domain.Models;

namespace Domain.ViewModels.Catalogue;

public class CatalogueViewModel
{
    public List<CategoryViewModel> Categories { get; set; }
    
    public List<ProductViewModel> Products { get; set; }
    
    public Guid CategoryId { get; set; }
}
