using Domain.Models;
using Domain.Response;
using Domain.ViewModels.Filter;

namespace Services.Interfaces;

public interface IProductService
{
    BaseResponse<List<Product>> GetProductByFilter(ProductFilter filter);
    
    Task<BaseResponse<Product>> GetProductById(Guid id);
}