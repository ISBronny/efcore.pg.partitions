using Brayko.EFCore.Partitions.PostgreSql.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brayko.EFCore.Partitions.PostgreSql.Extensions.BuilderExtensions;

public abstract class PartitionBuilder<TEntity, TKey, TBuilder> where TBuilder : PartitionBuilder<TEntity,TKey, TBuilder> where TEntity : class
{
    protected readonly EntityTypeBuilder<TEntity> EntityTypeBuilder;
    protected readonly PartitionInfo PartitionInfo;

    protected PartitionBuilder(EntityTypeBuilder<TEntity> entityType, PartitionInfo partitionInfo)
    {
        EntityTypeBuilder = entityType;
        PartitionInfo = partitionInfo;
        EntityTypeBuilder.HasAnnotation(AnnotationNames.Partition, PartitionInfo);
    }

    public virtual TBuilder HasDefaultPartition(string name)
    {
        return (TBuilder)this;
    }
}