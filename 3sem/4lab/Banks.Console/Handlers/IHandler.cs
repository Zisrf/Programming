using Banks.Services;

namespace Banks.Console.Handlers;

public interface IHandler
{
    IHandler? NextHandler { get; }

    IHandler SetNext(IHandler nextHandler);
    void Handle(string request, CentralBank centralBank);
}