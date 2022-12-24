using Banks.Services;

namespace Banks.Console.Handlers;

public class ExitHandler : Handler
{
    public override void Handle(string request, CentralBank centralBank)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(centralBank);

        if (request.ToLower() == "exit")
        {
            System.Environment.Exit(0);
        }

        NextHandler?.Handle(request, centralBank);
    }
}