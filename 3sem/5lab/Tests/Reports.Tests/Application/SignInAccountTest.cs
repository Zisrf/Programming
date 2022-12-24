using Reports.Application.Contracts.Employees;
using Reports.Application.Contracts.Entities;
using Reports.Application.Contracts;
using Reports.Application.Handlers.Employees;
using Reports.Application.Handlers.Entities;
using Reports.Application.Handlers;

namespace Reports.Tests.Application;

public class SignInAccountTest : ApplicationTest
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(17, 29)]
    [InlineData(999, 1000)]
    public void SignInInaccessibleAccount_ThrowException(int workerAccessLevel, int accountAccessLevel)
    {
        var createManagerHandler = new CreateManagerHandler(Context);
        var createWorkerHandler = new CreateWorkerHandler(Context);
        var createAccountHandler = new CreateAccountHandler(Context);
        var signInAccountHandler = new SignInAccountHandler(Context);

        Guid managerId = createManagerHandler.Handle(
            new CreateManager.Command(Guid.NewGuid().ToString()),
            CancellationToken.None
        ).Result.Manager.Id;

        Guid workerId = createWorkerHandler.Handle(
            new CreateWorker.Command(Guid.NewGuid().ToString(), workerAccessLevel, managerId),
            CancellationToken.None
        ).Result.Worker.Id;

        Guid accountId = createAccountHandler.Handle(
            new CreateAccount.Command(accountAccessLevel),
            CancellationToken.None
            ).Result.Account.Id;

        Assert.ThrowsAny<Exception>(()
            => signInAccountHandler.Handle(new SignInAccount.Command(workerId, accountId), CancellationToken.None).Result);
    }
}