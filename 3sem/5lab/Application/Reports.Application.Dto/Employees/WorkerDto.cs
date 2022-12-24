namespace Reports.Application.Dto.Employees;

public record WorkerDto(Guid Id, string Name, int AccessLevel)
    : EmployeeDto(Id, Name);