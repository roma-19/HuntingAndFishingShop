using System.Diagnostics;
using Domain.ViewModels.LoginAndRegistration;
using Microsoft.AspNetCore.Mvc;
using HuntingAndFishingShop.Models;

namespace HuntingAndFishingShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Main()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginViewModel model)
    {
        if (ModelState.IsValid) return Ok(model);
        var errors = ModelState.Values.SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return BadRequest(errors);
    }

    public IActionResult Register([FromBody] RegisterViewModel model)
    {
        if (ModelState.IsValid) return Ok(model);
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
}