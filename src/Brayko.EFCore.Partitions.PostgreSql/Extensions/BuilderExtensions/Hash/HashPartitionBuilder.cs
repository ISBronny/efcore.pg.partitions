using Brayko.EFCore.Partitions.PostgreSql.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brayko.EFCore.Partitions.PostgreSql.Extensions.BuilderExtensions.Hash;

public class HashPartitionBuilder<TEntity, TKey> : PartitionBuilder<TEntity, TKey, HashPartitionBuilder<TEntity, TKey>> where TEntity : class
{
    internal HashPartitionBuilder(EntityTypeBuilder<TEntity> entityType, PartitionInfo partitionInfo) : base(entityType, partitionInfo)
    {
    }
    public HashPartitionBuilder<TEntity, TKey> HasAllPartitions(int modulus)
    {
        return this;
    }
    
    public HashPartitionBuilder<TEntity, TKey> HasPartition(string name, int modulus, int remainder)
    {
        return this;
    }

    public override HashPartitionBuilder<TEntity, TKey> HasDefaultPartition(string name)
    {
        throw new InvalidOperationException("A hash-partitioned table may not have a default partition");
    }

    
}