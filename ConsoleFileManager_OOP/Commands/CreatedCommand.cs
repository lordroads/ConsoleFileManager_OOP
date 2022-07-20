using FileManagerOOP.Enums;
using FileManagerOOP.Interfaces.Command;
using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Models;
using System.Diagnostics;

namespace FileManagerOOP.Commands;

internal class CreatedCommand : NonTerminatingCommand, IParameterisedCommand
{
    private readonly ILogger _logger;
    private readonly StateActivity<StateConfig> _stateActivity;

    string Name { get; set; }

    public CreatedCommand(IView view, ILogger logger, StateActivity<StateConfig> stateActivity) : base(view)
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
                Name = GetParameter("Введите название файла или директории.");
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
            if (!string.IsNullOrWhiteSpace(parameters[0]))
            {
                Name = parameters[0];
            }
            else
            {
                Name = string.Empty;
            }
        }

        return this;
    }

    internal override bool InternalCommand()
    {
        string currentPath = Path.Combine(_stateActivity.CurrentState.SelectedPath, Name);

        string[] parseName = Name.Split('.');
        if (parseName.Length > 1)
        {
            try
            {
                if (!File.Exists(currentPath))
                {
                    File.Create(currentPath).Close();
                    View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, $"File created to path: \"{currentPath}\""));
                    View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Status command - OK!"));

                    return true;
                }
                else
                {
                    View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, $"File not created to path: \"{currentPath}\""));
                    View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, $"File exists {Name}"));
                    View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Status command - BAD!"));

                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.Log(ex);
                return false;
            }
        }
        else
        {
            try
            {
                Directory.CreateDirectory(currentPath);

                View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, $"File created to path: \"{currentPath}\""));
                View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Status command - OK!"));

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.Log(ex);
                return false;
            }
        }
    }
}
