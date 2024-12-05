using AutoMapper;
using Domain.ViewModels.Catalogue;
using Microsoft.AspNetCore.Mvc;
using Services.Implementation;
using Services.Interfaces;

namespace HuntingAndFishingShop.Controllers;

public class CatalogueController : Controller
{
    private readonly ICatalogueService _catalogueService;
    private IMapper _mapper {get; set;}

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });
    
    public CatalogueController(ICatalogueService catalogueService)
    {
        _catalogueService = catalogueService;
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
    
}
