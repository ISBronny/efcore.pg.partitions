using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Internal;

namespace Brayko.EFCore.Partitions.PostgreSql.Design;

public class PartitionsNpgsqlAnnotationsProvider : NpgsqlAnnotationProvider
{
    public PartitionsNpgsqlAnnotationsProvider(RelationalAnnotationProviderDependencies dependencies) : base(dependencies)
    {
    }

    
    public override IEnumerable<IAnnotation> For(ITable table, bool designTime)
    {
        foreach (var annotation in base.For(table, designTime)) yield return annotation;
        
        var entityType = (IEntityType)table.EntityTypeMappings.First().TypeBase;
        
        foreach (var annotation in entityType.GetAnnotations()
                     .Where(a => a.Name.StartsWith(AnnotationNames.Prefix, StringComparison.Ordinal)))
        {
            yield return annotation;
        }
    }
}