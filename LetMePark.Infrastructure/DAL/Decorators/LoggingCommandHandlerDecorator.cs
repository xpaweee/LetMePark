using LetMePark.Application.Abstractions;

namespace LetMePark.Infrastructure.DAL.Decorators;

public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;

    public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler)
    {
        _commandHandler = commandHandler;
    }
    
    public async Task HandleAsync(TCommand command)
    {
        Console.WriteLine(command.GetType().Name);
        await _commandHandler.HandleAsync(command);
    }
}