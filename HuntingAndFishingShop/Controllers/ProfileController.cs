using AutoMapper;
using Domain.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Implementation;
using Services.Interfaces;

namespace HuntingAndFishingShop.Controllers;

public class ProfileController : Controller
{
    private readonly IProfileService _profileService;
    
    private readonly IMapper _mapper;
    
    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
        _mapper = mapperConfiguration.CreateMapper();
    }
    
    public async Task<IActionResult> ProfilePage()
    {
        var email = User.Identity?.Name;
        if (string.IsNullOrEmpty(email))
        {
            return RedirectToAction("Login", "Home");
        }

        var user = await _profileService.GetUserByEmailAsync(email);

        var viewModel = new UserProfileViewModel
        {
            Name = user.Login,
            Email = user.Email,
            Password = user.Password,
            PhotoUrl = user.PathImage
        };
        
        var result = _mapper.Map<UserProfileViewModel>(viewModel);

        return View(result);
    }
}