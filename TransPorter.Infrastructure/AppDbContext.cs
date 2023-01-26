using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TransPorter.Domain;

namespace TransPorter.Infrastructure;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions options) : base(options) { }
}

//public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//{
//    public AppDbContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
//        optionsBuilder.UseSqlServer("server=localhost;database=TransPoster_DB;Trusted_Connection=True;Encrypt=False");

//        return new AppDbContext(optionsBuilder.Options);
//    }
//}