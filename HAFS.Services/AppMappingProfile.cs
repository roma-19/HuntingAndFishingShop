using AutoMapper;
using DAL.Storage;
using Domain.Models;
using Domain.ModelsDb;
using Domain.ViewModels;
using Domain.ViewModels.Cart;
using Domain.ViewModels.Catalogue;
using Domain.ViewModels.LoginAndRegistration;

namespace Services;

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
        
        //Корзина
        CreateMap<CartItem, CartItemDb>().ReverseMap();
        CreateMap<CartItem, CartViewModel>().ReverseMap();
    }
}

