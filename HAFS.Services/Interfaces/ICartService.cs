using Domain.Models;
using Domain.Response;
using Domain.ViewModels.Cart;

namespace Services.Interfaces;

public interface ICartService
{
    Task<BaseResponse<List<CartItem>>> GetCartItems(string userEmail);
    Task<BaseResponse<CartItem>> AddToCart(string userEmail, Guid productId, int quantity, decimal price, string imagePath);
    Task<BaseResponse<CartItem>> RemoveFromCart(string userEmail, Guid cartItemId);
    //Task<BaseResponse<CartItem>> UpdateQuantity(string userEmail, Guid cartItemId, int newQuantity);
}