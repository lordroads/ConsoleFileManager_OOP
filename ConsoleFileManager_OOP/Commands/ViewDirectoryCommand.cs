using FileManagerOOP.Models;
using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Interfaces.Command;
using System.Diagnostics;
using FileManagerOOP.Enums;

namespace FileManagerOOP.Commands;

internal class ViewDirectoryCommand : NonTerminatingCommand, IParameterisedCommand
{
    private readonly ILogger _logger;
    private readonly StateActivity<StateConfig> _stateActivity;
    public string PathDirectiry { get; set; }
    public ViewDirectoryCommand(StateActivity<StateConfig> stateActivity, IView view, ILogger logger) : base(view)
    {
        _logger = logger;
        _stateActivity = stateActivity;
    }

    internal override bool InternalCommand()
    {
        int _countItemOnPage = _stateActivity.CurrentState.CountItemOnPage;
        List<string> _dataDirs = new List<string>();
        List<string> _dataFiles = new List<string>();
        int countPages = 0;

        if (Directory.Exists(PathDirectiry))
        {
            _dataDirs = Directory.GetDirectories(PathDirectiry).ToList();
            _dataFiles = Directory.GetFiles(PathDirectiry).ToList();
            countPages = (_dataDirs.Count + _dataFiles.Count) / _countItemOnPage;

            _stateActivity.CurrentState.SelectedPath = PathDirectiry;
        }
        else
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, $"Не корректный путь - \"{PathDirectiry}\"\n"));
            Debug.WriteLine($"Нет такого пути - {PathDirectiry}");
            _logger.Log(new Exception($"Нет такого пути - {PathDirectiry}"));
            return false;
        }

        List<string> resultPage = new List<string>();

        _stateActivity.CurrentState.SelectedPage = 0;
        int startIndex = 0 * _countItemOnPage;
        int lastIndex = startIndex + _countItemOnPage;

        int allItem = _dataDirs.Count + _dataFiles.Count;

        for (int i = startIndex; i < lastIndex; i++)
        {
            if (i >= allItem)
            {
                break;
            }
            else if (i < _dataDirs.Count)
            {
                string dir = _dataDirs[i];
                resultPage.Add($"[]{Path.GetFileName(dir)}");
            }
            else if (i < allItem)
            {
                string file = _dataFiles[i - _dataDirs.Count];
                resultPage.Add($"  {Path.GetFileName(file)}");
            }
        }

        string strPageNumbers = "Page - ";

        for (int i = 0; i <= countPages; i++)
        {
            if (i == 0)
            {
                strPageNumbers += $"[{i + 1}] ";
            }
            else
            {
                strPageNumbers += $"{i + 1} ";
            }
        }

        resultPage.Add(strPageNumbers);

        View.AddView(ViewZone.HEADER, new Line(FormatLine.LONG, $"{_stateActivity.CurrentState.SelectedPath}", DateTime.Now.ToString("HH:mm:ss_dd.MM.yyyy")));
        foreach (string text in resultPage)
        {
            View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, text));
        }

        var info = Helper.GetInfo(PathDirectiry);
        foreach (var line in info)
        {
            View.AddView(ViewZone.FOOTER, new Line(FormatLine.CENTER, line));
        }

        return true;
    }

    public ProgrammCommand SetParameters(params string[] parameters)
    {
        if (parameters != null)
        {
            PathDirectiry = parameters[0];
        }
        
        return this;
    }

    public bool GetParameters()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(PathDirectiry))
            {
                PathDirectiry = GetParameter("Путь до директории.");
            }
        }
        catch (Exception ex)
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, ex.Message));
            _logger.Log(ex);
        }

        return !string.IsNullOrWhiteSpace(PathDirectiry);
    }
}