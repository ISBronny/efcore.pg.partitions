using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brayko.EFCore.Partitions.PostgreSql.Extensions.BuilderExtensions;

public abstract class PartitionConfigurator<TEntity> where TEntity : class
{
    protected EntityTypeBuilder<TEntity> EntityTypeBuilder { get; }
    protected string PartitionName { get; }

    internal PartitionConfigurator(EntityTypeBuilder<TEntity> entityTypeBuilder, string partitionName)
    {
        EntityTypeBuilder = entityTypeBuilder;
        PartitionName = partitionName;
    }
}