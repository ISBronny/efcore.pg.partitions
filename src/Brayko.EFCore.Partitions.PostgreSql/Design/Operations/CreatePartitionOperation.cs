using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Brayko.EFCore.Partitions.PostgreSql.Design.Operations;

public class CreatePartitionedTableOperation : CreateTableOperation
{
    public PartitionType PartitionType { get; set; }
    public string PartitionKey { get; set; } = null!;
}

[DebuggerDisplay("CREATE TABLE {Name} PARTITION OF {PartitionedTableName}")]
public class CreatePartitionOperation : TableOperation
{
    public virtual string PartitionedTableName { get; set; } = null!;
}

public class CreateDefaultPartitionOperation : CreatePartitionOperation
{
}

public class CreateListPartitionOperation : CreatePartitionOperation
{
    public List<object> Values { get; set; } = new();
}

public class CreateRangePartitionOperation : CreatePartitionOperation
{
    public object From { get; set; }
    public object To { get; set; }
}

public class CreateHashPartitionOperation : CreatePartitionOperation
{
    public int Modulus { get; set; }
    public int Remainder { get; set; }
}