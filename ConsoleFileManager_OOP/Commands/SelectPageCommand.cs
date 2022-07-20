using FileManagerOOP.Enums;
using FileManagerOOP.Interfaces.Command;
using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Models;
using System.Diagnostics;

namespace FileManagerOOP.Commands;

internal class SelectPageCommand : NonTerminatingCommand, IParameterisedCommand
{
    private readonly ILogger _logger;
    private readonly StateActivity<StateConfig> _stateActivity;

    public int Page { get; set; }
    public SelectPageCommand(IView view, ILogger logger, StateActivity<StateConfig> stateActivity) : base(view)
    {
        _logger = logger;
        _stateActivity = stateActivity;
    }

    public bool GetParameters()
    {
        try
        {
            if (Page == 0)
            {
                Page = Convert.ToInt32(GetParameter("Введите номер страницы."));
            }
        }
        catch (Exception ex)
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, ex.Message));
            _logger.Log(ex);
        }

        return Page != 0;
    }

    public ProgrammCommand SetParameters(params string[] parameters)
    {
        if (parameters != null)
        {
            if (int.TryParse(parameters[0], out int number))
            {
                Page = number;
            }
            else
            {
                Page = 0;
            } 
        }
        return this;
    }

    internal override bool InternalCommand()
    {
        int _countItemOnPage = _stateActivity.CurrentState.CountItemOnPage; //TODO: Как то надо получить!?
        List<string> _dataDirs = new List<string>();
        List<string> _dataFiles = new List<string>();
        int countPages = 0;

        if (Directory.Exists(_stateActivity.CurrentState.SelectedPath))
        {
            _dataDirs = Directory.GetDirectories(_stateActivity.CurrentState.SelectedPath).ToList();
            _dataFiles = Directory.GetFiles(_stateActivity.CurrentState.SelectedPath).ToList();
            countPages = (_dataDirs.Count + _dataFiles.Count) / _countItemOnPage;
        }
        else
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, $"Не корректный путь - \"{_stateActivity.CurrentState.SelectedPath}\"\n"));
            Debug.WriteLine($"Нет такого пути - {_stateActivity.CurrentState.SelectedPath}");
            _logger.Log(new Exception($"Нет такого пути - {_stateActivity.CurrentState.SelectedPath}"));
            return false;
        }

        List<string> resultPage = new List<string>();

        if (Page - 1 <= 0 || Page - 1 > countPages)
        {
            Page = 1;
        }

        _stateActivity.CurrentState.SelectedPage = Page - 1;
        int startIndex = _stateActivity.CurrentState.SelectedPage * _countItemOnPage;
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
            if (i == _stateActivity.CurrentState.SelectedPage)
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

        var info = Helper.GetInfo(_stateActivity.CurrentState.SelectedPath);
        foreach (var line in info)
        {
            View.AddView(ViewZone.FOOTER, new Line(FormatLine.CENTER, line));
        }

        return true;
    }
}
