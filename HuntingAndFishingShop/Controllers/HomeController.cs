using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using DAL;
using Domain.Models;
using Domain.ViewModels;
using Domain.ViewModels.LoginAndRegistration;
using Microsoft.AspNetCore.Mvc;
using HuntingAndFishingShop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Services.Implementation;
using Services.Interfaces;

namespace HuntingAndFishingShop.Controllers;

public class HomeController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ICatalogueService _catalogueService;

    private IMapper _mapper { get; set; }
    
    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });
    
    private readonly ILogger<HomeController> _logger;
    
    private readonly IWebHostEnvironment _appEnvironment;
    public HomeController(ILogger<HomeController> logger, IAccountService accountService, IWebHostEnvironment appEnvironment, ICatalogueService catalogueService)
    {
        _logger = logger;
        _mapper = mapperConfiguration.CreateMapper();
        _accountService = accountService;
        _appEnvironment = appEnvironment;
        _catalogueService = catalogueService;
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

            var confirm = _mapper.Map<ConfirmEmailViewModel>(model);
            
            var code = await _accountService.Register(user);
            
            confirm.GeneratedCode = code.Data;

            return Ok(confirm);
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

    [HttpPost]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel confirmEmailModel)
    {
        var user = _mapper.Map<User>(confirmEmailModel);
        
        var response = await _accountService.ConfirmEmail(user, confirmEmailModel.GeneratedCode, confirmEmailModel.CodeConfirm);

        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
            return Ok(confirmEmailModel);
        }
        ModelState.AddModelError("", response.Description);
        
        var errors = ModelState.Values.SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return BadRequest(errors);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task AuthenticationGoogle(string returnUrl = "/")
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse", new {returnUrl}),
                Parameters = { {"prompt", "select_account"} }
            });
    }

    public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
    {
        try
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result?.Succeeded == true)
            {
                User model = new User
                {
                    Login = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                    Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,
                    PathImage =
                        "/" + SaveImageInImageUser(result.Principal.FindFirst("picture")?.Value, result).Result ??
                        "/images/icons/def_profile_icon.png"
                };
                var response = await _accountService.IsCreatedAccount(model);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));
                    return Redirect(returnUrl);
                }
            }
            return BadRequest("Аутентификация не удалась");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    public async Task<string> SaveImageInImageUser(string imageUrl, AuthenticateResult result)
    {
        string filePath = "";
        if (!string.IsNullOrEmpty(imageUrl))
        {
            using (var httpClient = new HttpClient())
            {
                filePath = Path.Combine("ImageUser", $"{result.Principal.FindFirst(ClaimTypes.Email)?.Value}_profile_icon.png");
                
                var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                
                await System.IO.File.WriteAllBytesAsync(Path.Combine(_appEnvironment.WebRootPath, filePath), imageBytes);
            }
        }
        return filePath;
    }
}
