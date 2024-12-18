using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShishByzh.Application.Common.Interfaces;
using ShishByzh.Domain.Users;
using ShishByzh.Persistence.EntityTypeConfigurations;

namespace ShishByzh.Persistence
{
    public class ShishByzhDbContext(DbContextOptions<ShishByzhDbContext> options) 
        : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options), IShishByzhDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
