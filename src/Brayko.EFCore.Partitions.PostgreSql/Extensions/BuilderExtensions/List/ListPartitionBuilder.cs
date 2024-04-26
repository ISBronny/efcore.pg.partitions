using Brayko.EFCore.Partitions.PostgreSql.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brayko.EFCore.Partitions.PostgreSql.Extensions.BuilderExtensions.List;

public class ListPartitionBuilder<TEntity, TKey> : PartitionBuilder<TEntity, TKey, ListPartitionBuilder<TEntity, TKey>> where TEntity : class
{
    internal ListPartitionBuilder(EntityTypeBuilder<TEntity> entityType, PartitionInfo partitionInfo) : base(entityType, partitionInfo)
    {
    }
    public ListPartitionBuilder<TEntity, TKey> HasPartition(string name,  Action<ListPartitionConfigurator<TEntity, TKey>> configurePartition)
    {
        EntityTypeBuilder.Metadata.AddAnnotation(AnnotationNames.PartitionType, "List");
        var configurator = new ListPartitionConfigurator<TEntity, TKey>(EntityTypeBuilder, name);
        configurePartition(configurator);
        return this;
    }

    
}