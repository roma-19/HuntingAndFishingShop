using DAL.Interfaces;
using Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;


namespace DAL.Storage;

public class CategoryStorage : IBaseStorage<CategoryDb>
{
    private readonly ApplicationDbContext _db;

    public CategoryStorage(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task Add(CategoryDb item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(CategoryDb item)
    {
        _db.Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task<CategoryDb> Get(Guid id)
    {
        return await _db.CategoriesDb.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<CategoryDb> GetAll()
    {
        return _db.CategoriesDb;
    }

    public async Task<CategoryDb> Update(CategoryDb item)
    {
        _db.CategoriesDb.Update(item);
        await _db.SaveChangesAsync();

        return item;
    }
}