using FileManagerOOP.Enums;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Models;

namespace FileManagerOOP.Commands;

internal class UnknownCommand : NonTerminatingCommand
{
    public UnknownCommand(IView view) : base (view)
    {
    }

    internal override bool InternalCommand()
    {
        View.Clear();

        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Enter command \"help\"."));
        View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Unknown command."));

        return false;
    }
}
