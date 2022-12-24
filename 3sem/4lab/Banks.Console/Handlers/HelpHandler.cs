using Banks.Services;

namespace Banks.Console.Handlers;

public class HelpHandler : Handler
{
    private const string HelpMessage = "Commands: \n" +
                                       " * Help\n" +
                                       " * Exit\n" +
                                       " * Register client\n" +
                                       " * Show clients\n" +
                                       " * Create bank\n" +
                                       " * Show banks\n" +
                                       " * Create bank account";

    public override void Handle(string request, CentralBank centralBank)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(centralBank);

        if (request.ToLower() is "help" or "show help" or "print help")
        {
            System.Console.WriteLine(HelpMessage);
            return;
        }

        NextHandler?.Handle(request, centralBank);
    }
}