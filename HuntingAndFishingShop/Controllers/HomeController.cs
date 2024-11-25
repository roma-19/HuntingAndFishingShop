using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using DAL;
using Domain.Models;
using Domain.ViewModels.LoginAndRegistration;
using Microsoft.AspNetCore.Mvc;
using HuntingAndFishingShop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Services.Implementation;
using Services.Interfaces;

namespace HuntingAndFishingShop.Controllers;

public class HomeController : Controller
{
    private readonly IAccountService _accountService;

    private IMapper _mapper { get; set; }
    
    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });
    
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger, IAccountService accountService)
    {
        _logger = logger;
        _mapper = mapperConfiguration.CreateMapper();
        _accountService = accountService;
    }

    public IActionResult Main()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(model);
            
            var responce = await _accountService.Login(user);
            if (responce.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(responce.Data));
                return Ok(model);
            }
            ModelState.AddModelError("", responce.Description);
        }
        var errors = ModelState.Values.SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return BadRequest(errors);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(model);
            
            var responce = await _accountService.Register(user);
            if (responce.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(responce.Data));
                return Ok(model);
            }
            ModelState.AddModelError("", responce.Description);
        }
        var errors = ModelState.Values.SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return BadRequest(errors);
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Main", "Home");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}