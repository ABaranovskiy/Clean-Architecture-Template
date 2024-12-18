namespace ShishByzh.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>
        (this IQueryable queryable, IConfigurationProvider configuration, CancellationToken cancellationToken) 
            where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync(cancellationToken);
}
