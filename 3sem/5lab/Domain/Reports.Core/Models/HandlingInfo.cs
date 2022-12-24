namespace Reports.Core.Models;

public record HandlingInfo(
    Guid EmployeeId, 
    Guid MessageId, 
    Guid SourceId,
    DateTime Date
    );