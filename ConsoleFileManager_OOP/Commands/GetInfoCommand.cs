using FileManagerOOP.Enums;
using FileManagerOOP.Interfaces.Command;
using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Models;

namespace FileManagerOOP.Commands;

internal class GetInfoCommand : NonTerminatingCommand, IParameterisedCommand
{
    private readonly ILogger _logger;
    private readonly StateActivity<StateConfig> _stateActivity;

    public string Name { get; set; }

    public GetInfoCommand(IView view, ILogger logger, StateActivity<StateConfig> stateActivity) : base(view)
    {
        _logger = logger;
        _stateActivity = stateActivity;
    }

    public bool GetParameters()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = GetParameter("Имя директории или файла.");
            }
        }
        catch (Exception ex)
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, ex.Message));
            _logger.Log(ex);
        }

        return !string.IsNullOrWhiteSpace(Name);
    }

    public ProgrammCommand SetParameters(params string[] parameters)
    {
        if (parameters != null)
        {
            Name = parameters[0];
        }

        return this;
    }

    internal override bool InternalCommand()
    {
        string path = Path.Combine(_stateActivity.CurrentState.SelectedPath, Name);

        if (File.Exists(path) | Directory.Exists(path))
        {
            View.AddView(ViewZone.HEADER, new Line(FormatLine.LONG, $"{Name}", DateTime.Now.ToString("HH:mm:ss_dd.MM.yyyy")));

            View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, $"Selected - {Name}"));

            var info = Helper.GetInfo(path);
            foreach (var line in info)
            {
                View.AddView(ViewZone.FOOTER, new Line(FormatLine.CENTER, line));
            }

            return true;
        }
        else
        {
            View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, $"Такого файла или директории не существует: \"{Name}\""));
            View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Status command - BAD!"));

            return false;
        }
    }
}
