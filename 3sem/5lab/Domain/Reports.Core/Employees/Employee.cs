using Reports.Core.Models;
using RichEntity.Annotations;

namespace Reports.Core.Employees;

public abstract partial class Employee : IEntity<Guid>
{
    protected Employee(string name)
        : this(Guid.NewGuid())
    {
        ArgumentNullException.ThrowIfNull(name);

        Name = name;
    }

    public string Name { get; protected init; }
    public abstract IEnumerable<HandlingInfo> HandlingInfo { get; }
}