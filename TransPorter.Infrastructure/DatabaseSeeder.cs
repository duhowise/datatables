using TransPoster.Application.Interface;

namespace TransPorter.Infrastructure;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly AppDbContext _dbContext;
    public DatabaseSeeder(AppDbContext dbContext) => _dbContext = dbContext;

    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }
}
