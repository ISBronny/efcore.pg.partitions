using Brayko.EFCore.Partitions.PostgreSql.Design.Operations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Brayko.EFCore.Partitions.PostgreSql.Design;

#pragma warning disable EF1001
public class PartitionsMigrationsModelDiffer : MigrationsModelDiffer
{
    public PartitionsMigrationsModelDiffer(IRelationalTypeMappingSource typeMappingSource,
        IMigrationsAnnotationProvider migrationsAnnotationProvider,
        IRowIdentityMapFactory rowIdentityMapFactory,
        CommandBatchPreparerDependencies commandBatchPreparerDependencies) : base(typeMappingSource,
        migrationsAnnotationProvider,
        rowIdentityMapFactory,
        commandBatchPreparerDependencies)
    {
    }

    protected override IEnumerable<MigrationOperation> Diff(ITable source, ITable target, DiffContext diffContext)
    {
        // Вызываем базовую реализацию для обработки стандартных операций
        foreach (var operation in base.Diff(source, target, diffContext))
        {
            yield return operation;
        }
        
        var oldPartitionInfo = FindPartitionInfo(source);
        var newPartitionInfo = FindPartitionInfo(target);

       
    }

    private IEnumerable<MigrationOperation> Diff(PartitionInfo? source, PartitionInfo? target, ITable sourceTable, ITable targetTable, DiffContext diffContext)
    {
        if (source is null && target is not null)
        {
            if (target.DefaultPartition is not null)
            {
                yield return new CreateDefaultPartitionOperation()
                {
                    Name = target.DefaultPartition.Name,
                    PartitionedTableName = targetTable.Name,
                    Schema = targetTable.Schema,
                    IsDestructiveChange = false
                };
            }

            foreach (var partitionDetails in target.Partitions)
            {
                yield return target.PartitionType switch
                {
                    PartitionType.Range => new CreateRangePartitionOperation()
                    {
                        Name = partitionDetails.Name,
                        PartitionedTableName = targetTable.Name,
                        Schema = targetTable.Schema,
                        IsDestructiveChange = false,
                        
                        From = partitionDetails.From,
                        To = partitionDetails.To
                    },
                    PartitionType.List => new CreateListPartitionOperation()
                    {
                        Name = partitionDetails.Name,
                        PartitionedTableName = targetTable.Name,
                        Schema = targetTable.Schema,
                        IsDestructiveChange = false,
                        
                        Values = partitionDetails.Values
                    },
                    PartitionType.Hash => new CreateHashPartitionOperation()
                    {
                        Name = partitionDetails.Name,
                        PartitionedTableName = targetTable.Name,
                        Schema = targetTable.Schema,
                        IsDestructiveChange = false,
                        
                        Modulus = partitionDetails.Modulus,
                        Remainder = partitionDetails.Remainder
                    },
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
    
    private PartitionInfo FindPartitionInfo(ITable table)
    {
        // Псевдокод для получения информации о партиции из аннотаций таблицы
        var annotation = table.FindAnnotation(AnnotationNames.Partition);
        if (annotation != null)
        {
            return annotation.Value as PartitionInfo;
        }
        return null;
    }
}

#pragma warning restore EF1001
