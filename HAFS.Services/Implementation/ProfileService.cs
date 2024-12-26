using AutoMapper;
using DAL;
using DAL.Storage;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Implementation;

public class ProfileService : IProfileService
{
    private readonly UserStorage _userStorage;
    
    private IMapper _mapper { get; set; }
    
    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public ProfileService(UserStorage userStorage)
    {
        _mapper = mapperConfiguration.CreateMapper();
        _userStorage = userStorage;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
         var user = await _userStorage.GetAll().FirstOrDefaultAsync(u => u.Email == email);
         return _mapper.Map<User>(user);
    }
}