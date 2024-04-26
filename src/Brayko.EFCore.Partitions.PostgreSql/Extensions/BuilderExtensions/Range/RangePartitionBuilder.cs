using Brayko.EFCore.Partitions.PostgreSql.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brayko.EFCore.Partitions.PostgreSql.Extensions.BuilderExtensions.Range;

public class RangePartitionBuilder<TEntity, TKey> : PartitionBuilder<TEntity, TKey, RangePartitionBuilder<TEntity, TKey>> where TEntity : class
{
    internal RangePartitionBuilder(EntityTypeBuilder<TEntity> entityType, PartitionInfo partitionInfo) : base(entityType, partitionInfo)
    {
    }
    
    public RangePartitionBuilder<TEntity, TKey> HasPartition(string name,  Action<RangePartitionConfigurator<TEntity,TKey>> configurePartition)
    {
        var configurator = new RangePartitionConfigurator<TEntity, TKey>(EntityTypeBuilder, name);
        configurePartition(configurator);
        return this;
    }


    
}