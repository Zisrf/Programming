using Banks.Services;

namespace Banks.Console.Handlers;

public abstract class Handler : IHandler
{
    public IHandler? NextHandler { get; private set; } = null;

    public IHandler SetNext(IHandler nextHandler)
    {
        ArgumentNullException.ThrowIfNull(nextHandler);

        NextHandler = nextHandler;

        return nextHandler;
    }

    public abstract void Handle(string request, CentralBank centralBank);
}