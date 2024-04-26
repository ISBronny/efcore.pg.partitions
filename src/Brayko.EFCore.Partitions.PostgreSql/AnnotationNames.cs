namespace Brayko.EFCore.Partitions.PostgreSql;

public static class AnnotationNames
{
    public const string Prefix = "Partitions:";
    public const string PartitionKey = Prefix + "PartitionKey";
    public const string PartitionType = Prefix + "PartitionType";
    public const string PartitionValues = Prefix + "PartitionValues";
    public const string Partition = Prefix + "Partition";
}