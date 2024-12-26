using AutoMapper;
using DAL.Interfaces;
using Domain.Enum;
using Domain.Models;
using Domain.ModelsDb;
using Domain.Response;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Implementation;

public class CartService : ICartService
{ 
    private readonly IBaseStorage<CartItemDb> _cartItemStorage;
    
    private IMapper _mapper { get; set; }
    
    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public CartService(IBaseStorage<CartItemDb> cartItemStorage)
    {
        _cartItemStorage = cartItemStorage;
        _mapper = mapperConfiguration.CreateMapper();
    }
    
    public async Task<BaseResponse<List<CartItem>>> GetCartItems(string userEmail)
    {
        try
        {
            var cartItems = await _cartItemStorage.GetAll()
                .Where(c => c.UserEmail == userEmail)
                .Include(c => c.Product)
                .ToListAsync();
            
            var result = _mapper.Map<List<CartItem>>(cartItems);

            return new BaseResponse<List<CartItem>>
            {
                Data = result,
                Description = "Корзина успешно загружена",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<CartItem>>
            {
                Description = $"Ошибка при получении корзины: {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    
    public async Task<BaseResponse<CartItem>> AddToCart(string userEmail, Guid productId, int quantity, decimal price, string imagePath)
    {
        try
        {
            var existingCartItem = await _cartItemStorage.GetAll()
                .FirstOrDefaultAsync(c => c.UserEmail == userEmail && c.ProductId == productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                existingCartItem.Price = price;
                existingCartItem.ImagePath = imagePath;

                var updatedCartItem = await _cartItemStorage.Update(existingCartItem);
                var cartItemResponse = _mapper.Map<CartItem>(updatedCartItem);

                return new BaseResponse<CartItem>
                {
                    Data = cartItemResponse,
                    Description = "Количество товара в корзине обновлено",
                    StatusCode = StatusCode.Ok
                };
            }
            else
            {
                var newCartItemDb = new CartItemDb
                {
                    Id = Guid.NewGuid(),
                    UserEmail = userEmail,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = price,
                    ImagePath = imagePath
                };

                await _cartItemStorage.Add(newCartItemDb);
                var newCartItem = _mapper.Map<CartItem>(newCartItemDb);

                return new BaseResponse<CartItem>
                {
                    Data = newCartItem,
                    Description = "Товар успешно добавлен в корзину",
                    StatusCode = StatusCode.Ok
                };
            }
        }
        catch (Exception ex)
        {
            return new BaseResponse<CartItem>
            {
                Description = $"Ошибка при добавлении товара в корзину: {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }


    public async Task<BaseResponse<CartItem>> RemoveFromCart(string userEmail, Guid cartItemId)
    {
        try
        {
            var cartItemDb = await _cartItemStorage.Get(cartItemId);

            if (cartItemDb == null || cartItemDb.UserEmail != userEmail)
            {
                return new BaseResponse<CartItem>
                {
                    Description = "Товар не найден в корзине",
                    StatusCode = StatusCode.NotFound
                };
            }

            await _cartItemStorage.Delete(cartItemDb);
            var cartItem = _mapper.Map<CartItem>(cartItemDb);

            return new BaseResponse<CartItem>
            {
                Data = cartItem,
                Description = "Товар успешно удален из корзины",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<CartItem>
            {
                Description = $"Ошибка при удалении товара из корзины: {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    
    // public async Task<BaseResponse<CartItem>> UpdateQuantity(string userEmail, Guid cartItemId, int newQuantity)
    // {
    //     try
    //     {
    //         if (newQuantity <= 0)
    //         {
    //             return new BaseResponse<CartItem>
    //             {
    //                 Description = "Количество должно быть больше нуля",
    //                 StatusCode = StatusCode.BadRequest
    //             };
    //         }
    //
    //         var cartItemDb = await _cartItemStorage.Get(cartItemId);
    //
    //         if (cartItemDb == null || cartItemDb.UserEmail != userEmail)
    //         {
    //             return new BaseResponse<CartItem>
    //             {
    //                 Description = "Товар не найден в корзине",
    //                 StatusCode = StatusCode.NotFound
    //             };
    //         }
    //
    //         cartItemDb.Quantity = newQuantity;
    //         var updatedCartItem = await _cartItemStorage.Update(cartItemDb);
    //         var cartItemResponse = _mapper.Map<CartItem>(updatedCartItem);
    //
    //         return new BaseResponse<CartItem>
    //         {
    //             Data = cartItemResponse,
    //             Description = "Количество товара в корзине обновлено",
    //             StatusCode = StatusCode.Ok
    //         };
    //     }
    //     catch (Exception ex)
    //     {
    //         return new BaseResponse<CartItem>
    //         {
    //             Description = $"Ошибка при обновлении количества товара в корзине: {ex.Message}",
    //             StatusCode = StatusCode.InternalServerError
    //         };
    //     }
    // }
}