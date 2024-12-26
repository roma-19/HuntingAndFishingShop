using AutoMapper;
using Domain.ModelsDb;
using Domain.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Services;
using Services.Interfaces;

namespace HuntingAndFishingShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private IMapper _mapper { get; set; }
        
        private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            _mapper = mapperConfiguration.CreateMapper();
        }
        
        public async Task<IActionResult> CartPage()
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(emailClaim))
            {
                return RedirectToAction("Login", "Home");
            }

            var response = await _cartService.GetCartItems(emailClaim);
            
            var cartViewModels = _mapper.Map<List<CartViewModel>>(response.Data);

            return View(cartViewModels);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(emailClaim))
            {
                return RedirectToAction("Login", "Home");
            }

            if (request == null || request.Quantity <= 0 || request.ProductId == Guid.Empty)
            {
                TempData["Message"] = "Invalid request data.";
                return BadRequest("Invalid data.");
            }

            var response = await _cartService.AddToCart(emailClaim, request.ProductId, request.Quantity, request.Price, request.ImagePath);
            
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartRequest request)
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(emailClaim))
            {
                return Unauthorized(new { message = "User not logged in." });
            }

            var response = await _cartService.RemoveFromCart(emailClaim, request.CartItemId);

            return Ok();
        }
        
        // public class UpdateQuantityRequest
        // {
        //     public Guid CartItemId { get; set; }
        //     public int NewQuantity { get; set; }
        // }
        //
        // [HttpPost]
        // public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityRequest request)
        // {
        //     if (request.NewQuantity <= 0)
        //     {
        //         return BadRequest(new { message = "Quantity must be greater than zero." });
        //     }
        //
        //     var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
        //
        //     if (string.IsNullOrEmpty(emailClaim))
        //     {
        //         return Unauthorized(new { message = "User not logged in." });
        //     }
        //
        //     var response = await _cartService.UpdateQuantity(emailClaim, request.CartItemId, request.NewQuantity);
        //
        //     return Ok(new { message = response.Description });
        // }
    }
}
