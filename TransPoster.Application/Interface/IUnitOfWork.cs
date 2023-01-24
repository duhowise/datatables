using TransPorter.Domain.Shared;
using TransPorter.Shared.Interfaces;

namespace TransPoster.Application.Interface;

public interface IUnitOfWork : IDisposable
{
    IRepositoryAsync<T> Repository<T>() where T : class, IEntity;

    Task<int> Commit(CancellationToken cancellationToken);

    Task Rollback();
}