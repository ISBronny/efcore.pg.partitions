using Brayko.EFCore.Partitions.PostgreSql.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Design;

namespace Brayko.EFCore.Partitions.PostgreSql.Extensions;

#pragma warning disable EF1001


public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UsePartitions(
        this DbContextOptionsBuilder optionsBuilder)
    {
        return optionsBuilder
            .ReplaceService<IRelationalAnnotationProvider, PartitionsNpgsqlAnnotationsProvider>()
            .ReplaceService<ICSharpMigrationOperationGenerator, PartitionsCSharpMigrationOperationGenerator>()
            .ReplaceService<IMigrationsModelDiffer, PartitionsMigrationsModelDiffer>();
    }
}

#pragma warning restore EF1001
