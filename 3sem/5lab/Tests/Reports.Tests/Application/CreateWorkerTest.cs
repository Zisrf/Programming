using Reports.Application.Contracts.Employees;
using Reports.Application.Handlers.Employees;
using Reports.Core.Employees;
using Reports.DataAccess.Extensions;

namespace Reports.Tests.Application;

public class CreateWorkerTest : ApplicationTest
{
    [Fact]
    public void CreateWorkerAndManager_ManagerHaveSubordinate()
    {
        var createManagerHandler = new CreateManagerHandler(Context);
        var createWorkerHandler = new CreateWorkerHandler(Context);

        Guid managerId = createManagerHandler.Handle(
            new CreateManager.Command(Guid.NewGuid().ToString()),
            CancellationToken.None
            ).Result.Manager.Id;

        Guid workerId = createWorkerHandler.Handle(
            new CreateWorker.Command(Guid.NewGuid().ToString(), 0, managerId),
            CancellationToken.None
            ).Result.Worker.Id;

        Manager manager = Context.Managers.GetEntityByIdAsync(managerId, CancellationToken.None).Result;
        Worker worker = Context.Workers.GetEntityByIdAsync(workerId, CancellationToken.None).Result;

        Assert.Contains(worker, manager.Subordinates);
    }
}