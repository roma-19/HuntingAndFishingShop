using Domain.ModelsDb;

namespace DAL.Interfaces;

public interface IBaseStorage<T>
{
    Task Add(T item);
    
    Task<T> Update(T item);
    
    Task Delete(T item);
    
    Task<T> Get(Guid id);
    
    IQueryable<T> GetAll();
}

