using AutoMapper;
using Domain.Models;
using Domain.ModelsDb;
using Domain.ViewModels;
using Domain.ViewModels.LoginAndRegistration;

namespace Services.Implementation;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDb>().ReverseMap();

        CreateMap<User, LoginViewModel>().ReverseMap();
        
        CreateMap<User, RegisterViewModel>().ReverseMap();

        CreateMap<RegisterViewModel, ConfirmEmailViewModel>().ReverseMap();
        
        CreateMap<User, ConfirmEmailViewModel>().ReverseMap();
    }
}

