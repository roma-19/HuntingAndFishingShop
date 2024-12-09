using AutoMapper;
using Domain.ViewModels.Catalogue;
using Domain.ViewModels.Filter;
using Microsoft.AspNetCore.Mvc;
using Services.Implementation;
using Services.Interfaces;

namespace HuntingAndFishingShop.Controllers;

public class CatalogueController : Controller
{
    private readonly ICatalogueService _catalogueService;
    private readonly IProductService _productService;
    private IMapper _mapper {get; set;}

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });
    
    public CatalogueController(ICatalogueService catalogueService, IProductService productService)
    {
        _catalogueService = catalogueService;
        _productService = productService;
        _mapper = mapperConfiguration.CreateMapper();
    }
    
    public IActionResult CataloguePage()
    {
        var categories = _catalogueService.GetAllCategories();
        var products = _catalogueService.GetAllProducts();
        
        var catalogue = new CatalogueViewModel
        {
            Categories = _mapper.Map<List<CategoryViewModel>>(categories.Data),
            Products = _mapper.Map<List<ProductViewModel>>(products.Data),
        };
        return View(catalogue);
    }

    public async Task<IActionResult> ProductPage(Guid id)
    {
        var product = await _productService.GetProductById(id);
        
        var result = _mapper.Map<ProductViewModel>(product.Data);
        
        return View(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Filter([FromBody] ProductFilter filter)
    {
        var result = _productService.GetProductByFilter(filter);
        
        var filteredProducts = _mapper.Map<List<ProductViewModel>>(result.Data);
        
        return Json(filteredProducts);
    }
}
