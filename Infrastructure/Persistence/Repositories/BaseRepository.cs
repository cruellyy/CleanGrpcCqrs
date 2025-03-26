using Domain.Entities.Base;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class BaseRepository<T>(DatabaseContext context) : IBaseRepository<T> where T : BaseEntity
{
    public Task<IQueryable<T>> GetAll()
    {
        return Task.FromResult(context.Set<T>().Where(entity => entity.IsDeleted == false));
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().Where(entity => entity.IsDeleted == false && entity.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsTrackingAsync(int id)
    {
        return await context.Set<T>().AsTracking().Where(entity => entity.IsDeleted == false && entity.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await context.Set<T>().AddAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        context.Set<T>().Update(entity);
    }

    public void Update(List<T> entities)
    {
        entities.ForEach(entity => entity.UpdatedAt = DateTime.UtcNow);
        
        context.Set<T>().UpdateRange(entities);
    }

    public async Task<bool> SaveTransationAsync()
    {
        await context.SaveChangesAsync();
        return true;
    }
}