using TransPorter.Shared.Interfaces;

namespace TransPoster.Application.Interface;

public interface IRepositoryAsync<T> where T : class, IEntity
{
    IQueryable<T> Entities { get; }

    Task<T> GetByIdAsync(long id);

    Task<List<T>> GetAllAsync();

    Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

    Task<T> AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entity);

    Task UpdateAsync(T entity);

    Task UpdateRangeAsync(IEnumerable<T> existingEntities, CancellationToken cancellationToken);

    void Update(T entity);

    Task DeleteAsync(T entity);

    Task DeleteRangeAsync(params T[] entities);
}
