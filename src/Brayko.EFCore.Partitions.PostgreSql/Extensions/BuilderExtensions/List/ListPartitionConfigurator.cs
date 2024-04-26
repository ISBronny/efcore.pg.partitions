using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brayko.EFCore.Partitions.PostgreSql.Extensions.BuilderExtensions.List;

public class ListPartitionConfigurator<TEntity, TKey> : PartitionConfigurator<TEntity> where TEntity : class
{
    internal ListPartitionConfigurator(EntityTypeBuilder<TEntity> entityTypeBuilder, string partitionName) : base(entityTypeBuilder, partitionName)
    {
    }
    
    public void ForValuesInList(params TKey[] values)
    {
        EntityTypeBuilder.HasAnnotation(AnnotationNames.PartitionValues, values);
    }


   
}