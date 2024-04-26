using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brayko.EFCore.Partitions.PostgreSql.Extensions.BuilderExtensions.Range;

public class RangePartitionConfigurator<TEntity, TKey> :  PartitionConfigurator<TEntity> where TEntity : class
{
    internal RangePartitionConfigurator(EntityTypeBuilder<TEntity> entityTypeBuilder, string partitionName) : base(entityTypeBuilder, partitionName)
    {
    }
    
    public void ForValuesInRange(TKey start, TKey end)
    {
        
    }
}