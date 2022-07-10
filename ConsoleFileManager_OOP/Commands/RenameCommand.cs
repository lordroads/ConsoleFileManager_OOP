using FileManagerOOP.Enums;
using FileManagerOOP.Interfaces.Command;
using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Models;

namespace FileManagerOOP.Commands;

internal class RenameCommand : NonTerminatingCommand, IParameterisedCommand
{
    private readonly ILogger _logger;
    private readonly StateActivity<StateConfig> _stateActivity;

    string OldName { get; set; }
    string Rename { get; set; }

    public RenameCommand(IView view, ILogger logger, StateActivity<StateConfig> stateActivity) : base(view)
    {
        _logger = logger;
        _stateActivity = stateActivity;
    }

    public bool GetParameters()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(OldName))
            {
                OldName = GetParameter("Введите путь до файла или директории.");
            }
            if (string.IsNullOrWhiteSpace(Rename))
            {
                Rename = GetParameter("Введите новое название файла или директории.");
            }
        }
        catch (Exception ex)
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, ex.Message));
            _logger.Log(ex);
        }

        return !string.IsNullOrWhiteSpace(OldName) & !string.IsNullOrWhiteSpace(Rename);
    }

    public ProgrammCommand SetParameters(params string[] parameters)
    {
        if (parameters.Length > 1)
        {
            OldName = parameters[0];
            Rename = parameters[1];
        }
        else
        {
            OldName = parameters[0];
            Rename = string.Empty;
        }
        return this;
    }

    internal override bool InternalCommand()
    {
        string fullPath = Path.Combine(_stateActivity.CurrentState.SelectedPath, OldName);
        string newFullPath = Path.Combine(_stateActivity.CurrentState.SelectedPath, Rename);

        DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
        if (directoryInfo.Attributes == FileAttributes.Directory)
        {
            return RenameDir(fullPath, newFullPath);
        }

        return RenameFile(fullPath, newFullPath);
    }

    private bool RenameFile(string oldPath, string newPath)
    {
        if (File.Exists(oldPath))
        {
            File.Move(oldPath, newPath);
            View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, $"Файл {OldName} переименован на {Rename}"));
            View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Status command - OK!"));

            return true;
        }
        else
        {
            View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, $"Такого файла не существует: \"{oldPath}\""));
            View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Status command - BAD!"));

            return false;
        }
    }

    private bool RenameDir(string oldPath, string newPath)
    {
        if (Directory.Exists(oldPath))
        {
            Directory.Move(oldPath, newPath);
            View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, $"Директория {OldName} переименована на {Rename}"));
            View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Status command - OK!"));

            return true;
        }
        else
        {
            View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, $"Такой директории не существует: \"{oldPath}\""));
            View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Status command - BAD!"));

            return false;
        }
    }
}
