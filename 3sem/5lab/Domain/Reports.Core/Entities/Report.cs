using Reports.Core.Employees;
using Reports.Core.Models;
using RichEntity.Annotations;

namespace Reports.Core.Entities;

public partial class Report : IEntity<Guid>
{
    public Report(Manager author, IEnumerable<HandlingInfo> handlingInfo)
        : this(Guid.NewGuid())
    {
        ArgumentNullException.ThrowIfNull(author);
        ArgumentNullException.ThrowIfNull(handlingInfo);

        Date = DateTime.Now;

        Author = author;
        HandlingInfo = handlingInfo.ToList();
    }

    public DateTime Date { get; protected init; }
    public virtual Manager Author { get; protected init; }
    public virtual IReadOnlyCollection<HandlingInfo> HandlingInfo { get; }

    public IEnumerable<HandlingInfo> GetHandlingInfoInTimeSpan(DateTime startTime, TimeSpan duration)
    {
        return HandlingInfo.Where(x => x.Date >= startTime && x.Date <= startTime + duration);
    }
}