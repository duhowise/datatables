using Microsoft.EntityFrameworkCore;
using TransPorter.Domain.Shared;
using TransPorter.Shared.Interfaces;
using TransPoster.Application.Interface;

namespace TransPorter.Infrastructure;

public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class, IEntity
{
    private readonly DbContext _dbContext;

    public RepositoryAsync(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> Entities => _dbContext.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entity)
    {
        await _dbContext.Set<T>().AddRangeAsync(entity);
    }

    public Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(params T[] entities)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        return Task.CompletedTask;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext
                .Set<T>()
                .ToListAsync();
    }

    /// <summary>
    /// Find entity by given id
    ///
    /// Null if not found
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T> GetByIdAsync(long id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
    {
        return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);
    }

    //TODO: should be different I know :), will fix later
    public Task UpdateRangeAsync(IEnumerable<T> existingEntities, CancellationToken cancellationToken)
    {
        _dbContext.UpdateRange(existingEntities);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(T entity)
    {
        T exist = _dbContext.Set<T>().Find(entity.Id);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        return Task.CompletedTask;
    }
}

