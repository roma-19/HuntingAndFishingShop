using AutoMapper;
using DAL.Interfaces;
using Domain.Enum;
using Domain.Models;
using Domain.ModelsDb;
using Domain.Response;
using Domain.Validators;
using Domain.ViewModels.Filter;
using Services.Interfaces;

namespace Services.Implementation;

public class ProductService : IProductService
{
    private readonly IBaseStorage<ProductDb> _productDbStorage;
    
    private IMapper _mapper { get; set; }
    
    private ProductValidator _productValidator { get; set; }
    
    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public ProductService(IBaseStorage<ProductDb> productDbStorage)
    {
        _productDbStorage = productDbStorage;
        _productValidator = new ProductValidator();
        _mapper = mapperConfiguration.CreateMapper();
    }

    public BaseResponse<List<Product>> GetProductByFilter(ProductFilter filter)
    {
        try
        {
            var productFilter = _productDbStorage.GetAll().ToList();
            
            if (filter != null && productFilter != null)
            {
                if (filter.CategoryId != Guid.Empty)
                {
                    productFilter = productFilter.Where(p => p.CategoryId == filter.CategoryId).ToList();
                }
                
                if (filter.MaxPrice != 120000 || filter.MinPrice != 0)
                {
                    productFilter = productFilter.Where(p => p.Price < filter.MaxPrice && p.Price > filter.MinPrice).ToList();
                }
            }
            var result = _mapper.Map<List<Product>>(productFilter);
            return new BaseResponse<List<Product>>
            {
                Data = result,
                Description = "Отфильтрованные данные",
                StatusCode = StatusCode.Ok
            };

        }
        catch (Exception e)
        {
            return new BaseResponse<List<Product>>()
            {
                Description = e.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<Product>> GetProductById(Guid id)
    {
        try
        {
            var productDb = await _productDbStorage.Get(id);
            
            var result = _mapper.Map<Product>(productDb);

            if (result == null)
            {
                return new BaseResponse<Product>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.Ok
                };
            }

            return new BaseResponse<Product>()
            {
                Data = result,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Product>()
            {
                Description = e.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}