using System.Reflection;

namespace Brayko.EFCore.Partitions.PostgreSql.Design;

public class PartitionInfo
{
    public IReadOnlyList<MemberInfo> PartitionKey { get; set; }
    public PartitionType PartitionType { get; set; }
    public PartitionDetails? DefaultPartition { get; set; }
    public List<PartitionDetails> Partitions { get; set; }

    internal PartitionInfo(IReadOnlyList<MemberInfo> partitionKey, PartitionType partitionType)
    {
        PartitionKey = partitionKey;
        PartitionType = partitionType;
        Partitions = new List<PartitionDetails>();
    }

    public void AddPartition(PartitionDetails details)
    {
        if (Partitions.Any(x=>x.Name == details.Name))
        {
            throw new InvalidOperationException($"Партиция с именем '{details.Name}' уже существует.");
        }
        Partitions.Add(details);
    }
    
    public void AddDefaultPartition(PartitionDetails details)
    {
        if (details is not null)
            throw new InvalidOperationException("The default partition is already set");
        
        DefaultPartition = details;
    }
}