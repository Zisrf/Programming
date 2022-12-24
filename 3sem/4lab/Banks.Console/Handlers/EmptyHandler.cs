using Banks.Services;

namespace Banks.Console.Handlers;

public class EmptyHandler : Handler
{
    public override void Handle(string request, CentralBank centralBank)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(centralBank);

        NextHandler?.Handle(request, centralBank);
    }
}