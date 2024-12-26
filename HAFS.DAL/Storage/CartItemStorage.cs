using DAL.Interfaces;
using Domain.Models;
using Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage;

public class CartItemStorage : IBaseStorage<CartItemDb>
{
    private readonly ApplicationDbContext _db;

    public CartItemStorage(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task Add(CartItemDb item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(CartItemDb item)
    {
        _db.Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task<CartItemDb> Get(Guid id)
    {
        return await _db.CartItemsDb.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<CartItemDb> GetAll()
    {
        return _db.CartItemsDb;
    }

    public async Task<CartItemDb> Update(CartItemDb item)
    {
        _db.CartItemsDb.Update(item);
        await _db.SaveChangesAsync();

        return item;
    }
}