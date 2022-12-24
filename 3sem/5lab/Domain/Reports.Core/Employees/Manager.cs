using Reports.Core.Entities;
using Reports.Core.Models;
using Reports.Domain.Common.Exceptions.Employees;

namespace Reports.Core.Employees;

public partial class Manager : Employee
{
    private readonly HashSet<Employee> _subordinates;

    public Manager(string name)
        : base(name)
    {
        _subordinates = new HashSet<Employee>();
    }

    public virtual IReadOnlyCollection<Employee> Subordinates => _subordinates;

    public override IEnumerable<HandlingInfo> HandlingInfo
        => _subordinates.SelectMany(e => e.HandlingInfo);

    public void AddSubordinate(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (employee.Equals(this))
            throw InvalidManagerOperationException.OnAddHimselfToSubordinates();

        if (!_subordinates.Add(employee))
            throw InvalidManagerOperationException.OnAddExistingSubordinate();
    }

    public void RemoveSubordinate(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (!_subordinates.Remove(employee))
            throw InvalidManagerOperationException.OnRemoveNonExistentSubordinate();
    }

    public Report GetReport()
    {
        return new Report(this, HandlingInfo);
    }
}