using Brayko.EFCore.Partitions.PostgreSql.Design.Operations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Brayko.EFCore.Partitions.PostgreSql.Design;

public class PartitionsCSharpMigrationOperationGenerator : CSharpMigrationOperationGenerator
{
    public PartitionsCSharpMigrationOperationGenerator(CSharpMigrationOperationGeneratorDependencies dependencies) : base(dependencies)
    {
    }
    
    protected virtual void Generate(CreatePartitionedTableOperation operation, IndentedStringBuilder builder)
    {
        
    }

    protected virtual void Generate(CreatePartitionOperation operation, IndentedStringBuilder builder)
    {
    }
}