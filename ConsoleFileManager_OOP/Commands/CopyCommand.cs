using FileManagerOOP.Enums;
using FileManagerOOP.Interfaces.Command;
using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Models;
using System.Diagnostics;

namespace FileManagerOOP.Commands;

internal class CopyCommand : NonTerminatingCommand, IParameterisedCommand
{
    private readonly ILogger _logger;

    public string DirectoryOut { get; set; }
    public string DirectoryIn { get; set; }

    public CopyCommand(IView view, ILogger logger) : base(view)
    {
        _logger = logger;
    }

    public bool GetParameters()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(DirectoryOut))
            {
                DirectoryOut = GetParameter("Введите путь до файла или директории.");
            }
            if (string.IsNullOrWhiteSpace(DirectoryIn))
            {
                DirectoryIn = GetParameter("Введите путь куда копировать.");
            }
        }
        catch (Exception ex)
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, ex.Message));
            _logger.Log(ex);
        }

        return !string.IsNullOrWhiteSpace(DirectoryOut) & !string.IsNullOrWhiteSpace(DirectoryIn);
    }

    public ProgrammCommand SetParameters(params string[] parameters)
    {
        if (parameters != null)
        {
            if(parameters.Length > 1)
            {
                DirectoryOut = parameters[0];
                DirectoryIn = parameters[1];
            }
            else
            {
                DirectoryOut = parameters[0];
                DirectoryIn = string.Empty;
            }
            
        }
        return this;
    }

    internal override bool InternalCommand()
    {
        try
        {
            string filePath = DirectoryOut;
            string newPath = DirectoryIn;
            FileInfo fileInfo = new FileInfo(filePath);
            bool status = false;

            if (fileInfo.Exists)
            {
                status = CopyFile(fileInfo, newPath);
            }
            else
            {
                if (Directory.Exists(filePath))
                {
                    if (Directory.Exists(newPath))
                    {
                        status = CopyDir(filePath, newPath);
                    }
                    else
                    {
                        Directory.CreateDirectory(newPath);

                        status = CopyDir(filePath, newPath);
                    }
                }
            }

            View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, status ? "File or directory copy!" : "File or directory not copy!"));
            View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, status ? "Copy - OK!" : "Cpoy - BAD!"));

            return status;
        }
        catch (Exception ex)
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, ex.Message));
            _logger.Log(ex);
            return false;
        }
    }
    /// <summary>
    /// Копирование файлов.
    /// </summary>
    /// <param name="fileInfo">Путь к файлу</param>
    /// <param name="newPath">Путь куда копировать</param>
    /// <returns>Возвращает bool, true если все копирование прошло успешно, false если произошла ошибка при копировании или файл отсуствует.</returns>
    private bool CopyFile(FileInfo fileInfo, string newPath)
    {
        try
        {
            if (fileInfo.Exists)
            {
                if (Directory.Exists(newPath))
                {
                    fileInfo.CopyTo(Path.Combine(newPath, fileInfo.Name));

                    return true;
                }
                else
                {
                    Directory.CreateDirectory(newPath);
                    fileInfo.CopyTo(Path.Combine(newPath, fileInfo.Name));

                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            View.ViewWarningLine(new Line(FormatLine.DEFAULT, ex.Message));
            _logger.Log(ex);
            return false;
        }
    }
    /// <summary>
    /// Копирование каталогов с файлами.
    /// </summary>
    /// <param name="pathDir">Путь откуда копировать</param>
    /// <param name="newPath">Путь куда копировать</param>
    /// <returns>Возвращает bool, true если все копирование прошло успешно, false если произошла ошибка при копировании.</returns>
    private bool CopyDir(string pathDir, string newPath)
    {
        try
        {
            string[] dirs = Directory.GetDirectories(pathDir, "*", SearchOption.AllDirectories);
            string[] files = Directory.GetFiles(pathDir, "*.*", SearchOption.AllDirectories);

            for (int i = 0; i < dirs.Length; i++)
            {
                Directory.CreateDirectory(dirs[i].Replace(pathDir, newPath));
            }
            for (int j = 0; j < files.Length; j++)
            {
                FileInfo fileInfo = new FileInfo(files[j]);
                CopyFile(fileInfo, files[j].Replace(pathDir, newPath).Replace(fileInfo.Name, String.Empty));
            }

            View.AddView(ViewZone.FOOTER, new Line(FormatLine.DEFAULT, "Copy - OK!"));

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
