namespace Brayko.EFCore.Partitions.PostgreSql.Design;

public class PartitionDetails
{
    public string Name { get; }
    public List<object> Values { get; set; }
    public object From { get; set; }
    public object To { get; set; }
    public int Modulus { get; set; }
    public int Remainder { get; set; }

    internal PartitionDetails(string name)
    {
        Name = name;
    }

    internal static PartitionDetails ForList(string name, List<object> values)
    {
        return new PartitionDetails(name) { Values = values };
    }

    internal static PartitionDetails ForRange(string name, object minValue, object maxValue)
    {
        return new PartitionDetails(name) { From = minValue, To = maxValue };
    }

    internal static PartitionDetails ForHash(string name, int modulus, int remainder)
    {
        return new PartitionDetails(name)
        {
            Modulus = modulus,
            Remainder = remainder
        };
    }
}