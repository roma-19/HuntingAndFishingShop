using DAL.Interfaces;
using DAL.Storage;
using Domain.ModelsDb;
using Services;
using Services.Implementation;
using Services.Interfaces;

namespace HuntingAndFishingShop;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<UserStorage>();
        services.AddScoped<IBaseStorage<UserDb>, UserStorage>();
        services.AddScoped<IBaseStorage<CategoryDb>, CategoryStorage>();
        services.AddScoped<IBaseStorage<ProductDb>, ProductStorage>();
        services.AddScoped<IBaseStorage<CartItemDb>, CartItemStorage>();
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICatalogueService, CatalogueService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IProfileService, ProfileService>();

        services.AddControllersWithViews()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
    }
}

