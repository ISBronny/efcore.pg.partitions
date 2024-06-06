using Brayko.EFCore.Partitions.PostgreSql.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Brayko.EFCore.Partitions.PostgreSql.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var services = new ServiceCollection()
            .AddDbContext<TestDbContext>(x => x
                .UseNpgsql()
                .UseNpgsqlPartitions())
            .BuildServiceProvider();
        
    }
}

public class MyModel
{
    public string Category { get; set; }
    public DateOnly CreatedAt { get; set; }
}

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MyModel>().HasKey(x => new { x.Category, x.CreatedAt });
        
        modelBuilder.Entity<MyModel>().HasListPartitionKey(x => x.Category)
            .HasPartition("Computers", x => x.ForValuesInList("PC", "MacBook"))
            .HasPartition("Accessories", x => x.ForValuesInList("Keyboard", "Mouse"))
            .HasPartition("Software", x => x.ForValuesInList("Operating Systems", "Office Suite"));

        modelBuilder.Entity<MyModel>()
            .HasRangePartitionKey(x => x.CreatedAt)
            .HasPartition("VERY OLD DATA", x => x.ForValuesInRange(DateOnly.MinValue, new DateOnly(1990, 01, 01)));
       
        modelBuilder.Entity<MyModel>().HasHashPartitionKey(x => x.Category)
            .HasAllPartitions(modulus: 10);
    }
}