using Microsoft.EntityFrameworkCore;

namespace ShishByzh.Persistence;

public static class DbInitializer
{
    public static void Initialize(ShishByzhDbContext context)
    {
        context.Database.Migrate();
    }
}