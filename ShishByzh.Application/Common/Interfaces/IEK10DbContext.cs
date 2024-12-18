using ShishByzh.Domain.Users;

namespace ShishByzh.Application.Common.Interfaces;

public interface IShishByzhDbContext
{
    public DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
