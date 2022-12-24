using Banks.Services;

namespace Banks.Console.Handlers;

public class LastHandler : Handler
{
    public override void Handle(string request, CentralBank centralBank)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(centralBank);

        System.Console.WriteLine($"'{request}' is invalid request, try 'help' command");
    }
}