using TransPorter.Domain.Shared;

namespace TransPoster.Application.Interface;

public interface IUnitOfWork : IDisposable
{
    IRepositoryAsync<T> Repository<T>() where T : class, IEntity;

    Task<int> Commit(CancellationToken cancellationToken);

    Task Rollback();
}