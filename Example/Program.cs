using Brayko.EFCore.Partitions.PostgreSql.Extensions;
using Microsoft.EntityFrameworkCore;

await using var ctx = new PartitionsContext();
await ctx.Database.EnsureDeletedAsync();
await ctx.Database.EnsureCreatedAsync();

public class PartitionsContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<HashPartitionTable>()
            .HasHashPartitionKey(x => x.Name)
            .HasAllPartitions(5);

        modelBuilder.Entity<ListPartitionTable>()
            .HasListPartitionKey(x => x.Category)
            .HasDefaultPartition("ListPartitionTable_Default")
            .HasPartition("ListPartitionTable_Clothes", x => x.ForValuesInList("Boots", "Jacket"))
            .HasPartition("ListPartitionTable_Food", x => x.ForValuesInList("Pizza", "Burgers"));

        modelBuilder.Entity<RangePartitionTable>()
            .HasRangePartitionKey(x => x.CreatedAt)
            .HasDefaultPartition("RangePartitionTable_Default")
            .HasPartition("RangePartitionTable_A-C", x => x.ForValuesInRange( ));

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql(@"Host=localhost:5432;Username=postgres;Password=postgres;Database=test")
            .UseNpgsqlPartitions();
}

public class HashPartitionTable
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string Data { get; set; }
}

public class ListPartitionTable
{
    public int Id { get; set; }
    public string Category { get; set; }
    
    public string Data { get; set; }
}

public class RangePartitionTable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string Data { get; set; }
}