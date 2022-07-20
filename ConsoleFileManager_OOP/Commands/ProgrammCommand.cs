using FileManagerOOP.Interfaces.Command;
using FileManagerOOP.Interfaces.View;

namespace FileManagerOOP.Commands;

public abstract class ProgrammCommand
{
    private readonly bool _isTerminatingCommand;
    protected IView View { get; }

    internal ProgrammCommand(bool commandIsTerminating, IView view)
    {
        _isTerminatingCommand = commandIsTerminating;
        View = view;
    }

    public (bool wasSuccessful, bool shouldQuit) RunCommand()
    {
        if (this is IParameterisedCommand parameterisedCommand)
        {
            parameterisedCommand.GetParameters();
        }
        return (InternalCommand(), _isTerminatingCommand);
    }
    internal string GetParameter(string parameterName)
    {
        return View.ReadValue($"{parameterName}: ");
    }
    internal abstract bool InternalCommand();
}
