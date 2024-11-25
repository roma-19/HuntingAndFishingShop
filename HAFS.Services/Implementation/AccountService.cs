using System.Security.Claims;
using AutoMapper;
using DAL.Interfaces;
using Domain.Enum;
using Domain.Helpers;
using Domain.Models;
using Domain.ModelsDb;
using Domain.Response;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using FluentValidation;
using Domain.Validators;

namespace Services.Implementation;

public class AccountService : IAccountService
{
    private readonly IBaseStorage<UserDb> _userStorage;
    private IMapper _mapper { get; set; }
    
    private UserValidator _validationRules { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });
    public AccountService(IBaseStorage<UserDb> userStorage)
    {
        _userStorage = userStorage;
        _mapper = mapperConfiguration.CreateMapper();
        _validationRules = new UserValidator();
    }
    public async Task<BaseResponce<ClaimsIdentity>> Login(User model)
    {
        try
        {
            await _validationRules.ValidateAndThrowAsync(model);
            
            var userDb = await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
            if (userDb == null) return new BaseResponce<ClaimsIdentity>() { Description = "Пользователь не найден" };
            if (userDb.Password != HashPasswordHelper.HashPassword(model.Password))
                return new BaseResponce<ClaimsIdentity>() { Description = "Неверный пароль или почта" };
            var result = AuthenticateUserHelper.Authenticate(userDb);
            return new BaseResponce<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.Ok
            };
        }
        catch (ValidationException ex)
        {
            var errorsMessages = string.Join("| ", ex.Errors.Select(x => x.ErrorMessage));
            return new BaseResponce<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponce<ClaimsIdentity>> Register(User model)
    {
        try
        {
            model.PathImage = "~/images/icons/profile_icon.png";
            model.Password = HashPasswordHelper.HashPassword(model.Password);
            
            var userDb = _mapper.Map<UserDb>(model);
            
            await _validationRules.ValidateAndThrowAsync(model);
            
            if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) != null)
            {
                return new BaseResponce<ClaimsIdentity>() { Description = "Пользователь с такой почтой уже есть" };
            }
            
            await _userStorage.Add(userDb);
            
            var result = AuthenticateUserHelper.Authenticate(userDb);
            return new BaseResponce<ClaimsIdentity>()
            {
                Data = result,
                Description = "Пользователь зарегистрирован",
                StatusCode = StatusCode.Ok
            };
        }
        catch (ValidationException ex)
        {
            var errorsMessages = string.Join("| ", ex.Errors.Select(x => x.ErrorMessage));
            return new BaseResponce<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}