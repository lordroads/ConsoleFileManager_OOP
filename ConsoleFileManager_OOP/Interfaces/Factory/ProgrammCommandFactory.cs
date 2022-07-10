using FileManagerOOP.Models;
using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Commands;

namespace FileManagerOOP.Interfaces.Factory;

internal class ProgrammCommandFactory : IProgrammCommandFactory
{
    private readonly IView _view;
    private readonly ILogger _logger;
    private readonly StateActivity<StateConfig> _stateActivity;
    public ProgrammCommandFactory(StateActivity<StateConfig> stateActivity, IView view, ILogger logger)
    {
        _view = view;
        _logger = logger;
        _stateActivity = stateActivity;
    }
    public ProgrammCommand GetCommand(string command)
    {
        return command.ToLower() switch
        {
            "view" => new ViewDirectoryCommand(_stateActivity, _view, _logger),

            "page" => new SelectPageCommand(_view, _logger, _stateActivity),
            "p" => new SelectPageCommand(_view, _logger, _stateActivity),

            "copy" => new CopyCommand(_view, _logger),
            "c" => new CopyCommand(_view, _logger),

            "delete" => new DeleteCommand(_view, _logger, _stateActivity),
            "d" => new DeleteCommand(_view, _logger, _stateActivity),

            "create" => new CreatedCommand(_view, _logger, _stateActivity),
            "cr" => new CreatedCommand(_view, _logger, _stateActivity),

            "rename" => new RenameCommand(_view, _logger, _stateActivity),
            "re" => new RenameCommand(_view, _logger, _stateActivity),

            "info" => new GetInfoCommand(_view, _logger, _stateActivity),
            "i" => new GetInfoCommand(_view, _logger, _stateActivity),

            "help" => new HelpCommand(_view),
            "h" => new HelpCommand(_view),

            "quit" => new QuitCommand(_view),
            "q" => new QuitCommand(_view),

            _ => new UnknownCommand(_view)
        };
    }
}
