using System.Linq.Expressions;
using System.Reflection;
using Brayko.EFCore.Partitions.PostgreSql.Design;
using Brayko.EFCore.Partitions.PostgreSql.Extensions.BuilderExtensions.Hash;
using Brayko.EFCore.Partitions.PostgreSql.Extensions.BuilderExtensions.List;
using Brayko.EFCore.Partitions.PostgreSql.Extensions.BuilderExtensions.Range;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brayko.EFCore.Partitions.PostgreSql.Extensions;

public static class PartitioningExtensions
{
    public static RangePartitionBuilder<TEntity, TKey> HasRangePartitionKey<TEntity, TKey>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity, TKey>> key)
        where TEntity : class
    {
        var partition = new PartitionInfo(key.GetMemberAccessList(), PartitionType.Range);
        return new RangePartitionBuilder<TEntity, TKey>(entity, partition);
    }
    
    public static ListPartitionBuilder<TEntity, TKey> HasListPartitionKey<TEntity, TKey>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity, TKey>> key)
        where TEntity : class
    {
        var partition = new PartitionInfo(key.GetMemberAccessList(), PartitionType.List);
        return new ListPartitionBuilder<TEntity, TKey>(entity, partition);
    }
    
    public static HashPartitionBuilder<TEntity, TKey> HasHashPartitionKey<TEntity, TKey>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity, TKey>> key)
        where TEntity : class
    {
        var partition = new PartitionInfo(key.GetMemberAccessList(), PartitionType.List);
        return new HashPartitionBuilder<TEntity, TKey>(entity, partition);
    }
}