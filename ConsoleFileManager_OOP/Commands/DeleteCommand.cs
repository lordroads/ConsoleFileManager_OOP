using FileManagerOOP.Enums;
using FileManagerOOP.Interfaces.Command;
using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Models;

namespace FileManagerOOP.Commands;

internal class DeleteCommand : NonTerminatingCommand, IParameterisedCommand
{
    private readonly ILogger _logger;
    private readonly StateActivity<StateConfig> _stateActivity;

    public string PathToDelete { get; set; }

    public DeleteCommand(IView view, ILogger logger, StateActivity<StateConfig> stateActivity) : base(view)
    {
        _logger = logger;
        _stateActivity = stateActivity;
    }

    public bool GetParameters()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(PathToDelete))
            {
                PathToDelete = GetParameter("Путь до директории или файла.");
            }
        }
        catch (Exception ex)
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, ex.Message));
            _logger.Log(ex);
        }

        return !string.IsNullOrWhiteSpace(PathToDelete);
    }

    public ProgrammCommand SetParameters(params string[] parameters)
    {
        if (parameters != null)
        {
            PathToDelete = parameters[0];
        }

        return this;
    }

    internal override bool InternalCommand()
    {
        bool status = Delete(PathToDelete);

        if (PathToDelete == _stateActivity.CurrentState.SelectedPath)
        {
            _stateActivity.CurrentState.SelectedPath = _stateActivity.CurrentState.SelectedPath
                .Replace(Path.GetFileName(_stateActivity.CurrentState.SelectedPath), string.Empty)
                .TrimEnd('\\');

            _stateActivity.CurrentState.SelectedPage = 0;
        }

        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, status ? "File or directory deleted!" : "File or directory not deleted!"));
        View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, status ? "Delete - OK!" : "Delete - BAD!"));
        return status;
    }
    /// <summary>
    /// Удаление файлов и катологов
    /// </summary>
    /// <param name="file">Путь к файлу или каталогу</param>
    /// <returns>При удачном удалении возращает true, в противном случае false.</returns>
    private bool Delete(string path)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        if (directoryInfo.Attributes == FileAttributes.Directory)
        {
            return DeleteDir(path);
        }

        return DeleteFile(path);
    }
    /// <summary>
    /// Удаление каталогов с файлами.
    /// </summary>
    /// <param name="path">Путь к каталогу</param>
    /// <returns>Возвращает bool, true если все удаление прошло успешно, false если произошла ошибка при удалении.</returns>
    private bool DeleteDir(string path)
    {
        try
        {
            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }
            for (int j = dirs.Length - 1; j >= 0; j--)
            {
                Directory.Delete(dirs[j]);
            }
            Directory.Delete(path);

            return true;
        }
        catch (Exception ex)
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, ex.Message));
            _logger.Log(ex);
            return false;
        }
    }
    /// <summary>
    /// Удаление файлов.
    /// </summary>
    /// <param name="pathFile">Путь к файлу.</param>
    /// <returns>Возвращает bool, true если все удаление прошло успешно, false если произошла ошибка при удалении.</returns>
    private bool DeleteFile(string pathFile)
    {
        try
        {
            if (File.Exists(pathFile))
            {
                File.Delete(pathFile);
            }

            return true;
        }
        catch (Exception ex)
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, ex.Message));
            _logger.Log(ex);
            return false;
        }
    }
}
