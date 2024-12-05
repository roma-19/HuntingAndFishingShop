using Domain.Models;
using Domain.Response;

namespace Services.Interfaces;

public interface ICatalogueService
{
    BaseResponse<List<Category>> GetAllCategories();
    
    BaseResponse<List<Product>> GetAllProducts();
}

