namespace Reports.Application.Common.Exceptions.NotFound;

public class EntityNotFoundException<T> : ReportsApplicationException
{
    public EntityNotFoundException(Guid id)
        : base($"{typeof(T).Name} with id {id} was not found") { }
}