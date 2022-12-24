using Reports.Core.Entities;
using Reports.Core.Messages;
using Reports.Core.MessageSources;
using Reports.Core.Models;
using Reports.Core.ValueObjects;
using Reports.Domain.Common.Exceptions.Employees;

namespace Reports.Core.Employees;

public partial class Worker : Employee
{
    private readonly List<HandlingInfo> _handlingInfo;

    public Worker(string name, AccessLevel accessLevel)
        : base(name)
    {
        Account = null;

        AccessLevel = accessLevel;

        _handlingInfo = new List<HandlingInfo>();
    }

    public AccessLevel AccessLevel { get; protected init; }
    public virtual Account? Account { get; private set; }
    public override IEnumerable<HandlingInfo> HandlingInfo => _handlingInfo;

    public void SignIn(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (!AccessLevel.CanAccess(account.AccessLevel))
            throw InvalidWorkerOperationException.OnLackOfAccessLevel();

        Account = account;
    }

    public void HandleMessage(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        MessageSource? messageSource = Account?.AvailableSources.FirstOrDefault(s => s.Messages.Contains(message));

        if (messageSource is null)
            throw InvalidWorkerOperationException.OnHandlingIncorrectMessage();

        message.Handle();

        _handlingInfo.Add(new HandlingInfo(Id, message.Id, messageSource.Id, DateTime.Now));
    }
}