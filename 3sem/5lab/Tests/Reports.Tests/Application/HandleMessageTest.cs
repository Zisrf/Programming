using Reports.Application.Contracts.Employees;
using Reports.Application.Contracts.Entities;
using Reports.Application.Contracts.Messages;
using Reports.Application.Contracts.MessageSources;
using Reports.Application.Contracts;
using Reports.Application.Handlers.Employees;
using Reports.Application.Handlers.Entities;
using Reports.Application.Handlers.Messages;
using Reports.Application.Handlers.MessageSources;
using Reports.Application.Handlers;
using Reports.Core.Employees;
using Reports.Core.Messages;
using Reports.Core.Models;
using Reports.DataAccess.Extensions;

namespace Reports.Tests.Application;

public class HandleMessageTest : ApplicationTest
{
    [Fact]
    public void HandleMessage_InfoChanged()
    {
        var createManagerHandler = new CreateManagerHandler(Context);
        var createWorkerHandler = new CreateWorkerHandler(Context);
        var createAccountHandler = new CreateAccountHandler(Context);
        var signInAccountHandler = new SignInAccountHandler(Context);
        var createEmailMessageSourceHandler = new CreateEmailMessageSourceHandler(Context);
        var receiveEmailMessageHandler = new ReceiveEmailMessageHandler(Context);
        var handleMessageHandler = new HandleMessageHandler(Context);

        Guid managerId = createManagerHandler.Handle(
            new CreateManager.Command(Guid.NewGuid().ToString()),
            CancellationToken.None
        ).Result.Manager.Id;

        Guid workerId = createWorkerHandler.Handle(
            new CreateWorker.Command(Guid.NewGuid().ToString(), 0, managerId),
            CancellationToken.None
        ).Result.Worker.Id;

        Guid accountId = createAccountHandler.Handle(
            new CreateAccount.Command(0),
            CancellationToken.None
        ).Result.Account.Id;

        _ = signInAccountHandler.Handle(
            new SignInAccount.Command(workerId, accountId),
            CancellationToken.None
        ).Result;

        Guid sourceId = createEmailMessageSourceHandler.Handle(
            new CreateEmailMessageSource.Command(Guid.NewGuid().ToString(), accountId),
            CancellationToken.None
        ).Result.MessageSource.Id;

        Guid messageId = receiveEmailMessageHandler.Handle(
            new ReceiveEmailMessage.Command(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), sourceId),
            CancellationToken.None
        ).Result.Message.Id;

        _ = handleMessageHandler.Handle(
            new HandleMessage.Command(workerId, messageId),
            CancellationToken.None
        ).Result;

        Message message = Context.Messages.GetEntityByIdAsync(messageId, CancellationToken.None).Result;
        Assert.Equal(MessageStatus.Handled, message.Status);

        Worker worker = Context.Workers.GetEntityByIdAsync(workerId, CancellationToken.None).Result;
        Assert.Single(worker.HandlingInfo);

        Manager manager = Context.Managers.GetEntityByIdAsync(managerId, CancellationToken.None).Result;
        Assert.Single(manager.HandlingInfo);
    }
}