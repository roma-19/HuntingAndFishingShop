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
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();

        services.AddControllersWithViews().AddDataAnnotationsLocalization().AddViewLocalization();
    }
}