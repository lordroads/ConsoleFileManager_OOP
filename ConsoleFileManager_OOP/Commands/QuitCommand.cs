using FileManagerOOP.Models;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Enums;

namespace FileManagerOOP.Commands;

internal class QuitCommand : ProgrammCommand
{
    public QuitCommand(IView view) : base(true, view)
    {
    }

    internal override bool InternalCommand()
    {
        View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Good Bye my friend. =)"));
        return true;
    }
}
