using Domain.Entities.Base;

namespace Domain.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<IQueryable<T>> GetAll();
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsTrackingAsync(int id);
    Task CreateAsync(T entity);
    Task DeleteAsync(T entity);
    void Update(T entity);
    void Update(List<T> entities);
    Task<bool> SaveTransationAsync();
}