using FileManagerOOP.Interfaces.View;

namespace FileManagerOOP.Commands;

internal abstract class NonTerminatingCommand : ProgrammCommand
{
    protected NonTerminatingCommand(IView view) : base(false, view)
    {
    }
}
