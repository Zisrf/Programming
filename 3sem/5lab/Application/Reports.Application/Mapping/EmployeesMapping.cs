using Reports.Application.Dto.Employees;
using Reports.Core.Employees;

namespace Reports.Application.Mapping;

public static class EmployeesMapping
{
    public static WorkerDto AsDto(this Worker worker)
    {
        return new WorkerDto(worker.Id, worker.Name, (int)worker.AccessLevel);
    }

    public static ManagerDto AsDto(this Manager manager)
    {
        return new ManagerDto(manager.Id, manager.Name);
    }
}