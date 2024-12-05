using AutoMapper;
using DAL.Interfaces;
using Domain.Enum;
using Domain.Models;
using Domain.ModelsDb;
using Domain.Response;
using Domain.Validators;
using Services.Interfaces;

namespace Services.Implementation;

public class CatalogueService : ICatalogueService
{
    private readonly IBaseStorage<CategoryDb> _categoryStorage;
    
    private IMapper _mapper { get; set; }
    private CategoryValidator _categoryValidator { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });
    
    private readonly IBaseStorage<ProductDb> _productStorage;
    private ProductValidator _productValidator { get; set; }
    public CatalogueService(IBaseStorage<CategoryDb> categoryStorage, IBaseStorage<ProductDb> productStorage)
    {
        _categoryStorage = categoryStorage;
        _productStorage = productStorage;
        _categoryValidator = new CategoryValidator();
        _productValidator = new ProductValidator();
        _mapper = mapperConfiguration.CreateMapper();
    }

    public BaseResponse<List<Category>> GetAllCategories()
    {
        try
        {
            var categoriesDb = _categoryStorage.GetAll().ToList();
            var result = _mapper.Map<List<Category>>(categoriesDb);
            if (result.Count == 0)
            {
                return new BaseResponse<List<Category>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.Ok
                };
            }
            return new BaseResponse<List<Category>>()
            {
                Data = result,
                StatusCode = StatusCode.Ok,
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Category>>()
            {
                Description = e.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public BaseResponse<List<Product>> GetAllProducts()
    {
        try
        {
            var productDb = _productStorage.GetAll().ToList();
            var result = _mapper.Map<List<Product>>(productDb);
            if (result.Count == 0)
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.Ok
                };
            }
            return new BaseResponse<List<Product>>()
            {
                Data = result,
                StatusCode = StatusCode.Ok,
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
}
