using DAL.Interfaces;
using Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage;

public class OrderStorage : IBaseStorage<OrderDb>
{
    private readonly ApplicationDbContext _db;

    public OrderStorage(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task Add(OrderDb item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(OrderDb item)
    {
        _db.Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task<OrderDb> Get(Guid id)
    {
        return await _db.OrdersDb.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<OrderDb> GetAll()
    {
        return _db.OrdersDb;
    }

    public async Task<OrderDb> Update(OrderDb item)
    {
        _db.OrdersDb.Update(item);
        await _db.SaveChangesAsync();

        return item;
    }
}