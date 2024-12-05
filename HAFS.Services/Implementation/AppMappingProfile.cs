using AutoMapper;
using Domain.Models;
using Domain.ModelsDb;
using Domain.ViewModels;
using Domain.ViewModels.Catalogue;
using Domain.ViewModels.LoginAndRegistration;

namespace Services.Implementation;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        //Регистрация и вход
        CreateMap<User, UserDb>().ReverseMap();
        CreateMap<User, LoginViewModel>().ReverseMap();
        CreateMap<User, RegisterViewModel>().ReverseMap();

        //Подтверждение почты
        CreateMap<RegisterViewModel, ConfirmEmailViewModel>().ReverseMap();
        CreateMap<User, ConfirmEmailViewModel>().ReverseMap();
        
        //Категории
        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Category, CategoryDb>().ReverseMap();
        
        //Товары
        CreateMap<Product, ProductDb>().ReverseMap();
        CreateMap<Product, ProductViewModel>().ReverseMap();
    }
}

