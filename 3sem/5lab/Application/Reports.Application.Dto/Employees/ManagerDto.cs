namespace Reports.Application.Dto.Employees;

public record ManagerDto(Guid Id, string Name)
    : EmployeeDto(Id, Name);