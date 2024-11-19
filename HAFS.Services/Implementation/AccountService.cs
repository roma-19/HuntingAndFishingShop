using AutoMapper;
using DAL.Interfaces;
using Domain.Enum;
using Domain.Models;
using Domain.ModelsDb;
using Domain.Response;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Implementation;

public class AccountService : IAccountService
{
    private readonly IBaseStorage<UserDb> _userStorage;
    
    private IMapper _mapper { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public AccountService(IBaseStorage<UserDb> userStorage)
    {
        _userStorage = userStorage;
        _mapper = mapperConfiguration.CreateMapper();
    }
    
    public async Task<BaseResponce<User>> Login(User model)
    {
        try
        {
            var userDb = _mapper.Map<UserDb>(model);

            if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) == null)
            {
                return new BaseResponce<User>()
                {
                    Description = "Пользователь не найден"
                };
            }

            if (userDb.Password != model.Password)
            {
                return new BaseResponce<User>()
                {
                    Description = "Неверный пароль или почта"
                };
            }

            return new BaseResponce<User>()
            {
                Data = model,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponce<User>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponce<User>> Register(User model)
    {
        try
        {
            model.PathImage = "";
            
            var userDb = _mapper.Map<UserDb>(model);

            if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) != null)
            {
                return new BaseResponce<User>()
                {
                    Description = "Пользователь с такой почтой уже есть"
                };
            }
            
            await _userStorage.Add(userDb);

            return new BaseResponce<User>()
            {
                Data = model,
                Description = "Пользователь зарегистрирован",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponce<User>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}