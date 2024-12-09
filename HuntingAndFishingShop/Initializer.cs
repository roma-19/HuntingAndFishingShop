using DAL.Interfaces;
using DAL.Storage;
using Domain.ModelsDb;
using Services.Implementation;
using Services.Interfaces;

namespace HuntingAndFishingShop;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseStorage<UserDb>, UserStorage>();
        services.AddScoped<IBaseStorage<CategoryDb>, CategoryStorage>();
        services.AddScoped<IBaseStorage<ProductDb>, ProductStorage>();
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICatalogueService, CatalogueService>();
        services.AddScoped<IProductService, ProductService>();

        services.AddControllersWithViews()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
    }
}

