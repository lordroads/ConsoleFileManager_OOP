using FileManagerOOP.Enums;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Models;

namespace FileManagerOOP.Commands;

internal class HelpCommand : NonTerminatingCommand
{
    public HelpCommand(IView view) : base(view)
    {
    }

    internal override bool InternalCommand()
    {
        
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "ИСПОЛЬЗУЕМЫЕ КОМАНДЫ:"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, " "));

        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "[view] - Выводит на экран указаную директорию. Если путь содержит пробелы, путь необходимо указатьв ковычках."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Пример:"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Command > view D:\\MyFolder"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, " "));

        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "[page (p)] - Переходит на указаную страницу, если такая имеется."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Пример:"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Command > page 2"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, " "));

        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "[copy (c)] - Копирует указаный файл или директория по указаному пути."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Пример:"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Command > copy D:\\MyFolder D:\\SomeFolder"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, " "));

        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "[delete (d)] - Удаляет указаный файл или директория по указаному пути."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Пример:"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Command > delete D:\\MyFolder"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, " "));

        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "[create (cr)] - Создает указаный файл или директория по указаному пути."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Пример:"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Command > create D:\\MyFolder\\newFile.txt"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, " "));

        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "[rename (re)] - Переименовывает указаный файл или директорию."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "НЕОБХОДИМО находится в директории указываемого файла или директории."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Пример:"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Command > re newFile.txt README.md"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, " "));

        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "[info (i)] - Выводит на экран информацию об указаном файле или директории."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "НЕОБХОДИМО находится в директории указываемого файла или директории."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Пример:"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, "Command > re newFile.txt README.md"));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, " "));

        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "[help] - Показывает какие команды есть у программы."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, " "));

        View.AddView(ViewZone.BODY, new Line(FormatLine.DEFAULT, "[quit] - Выход из программы."));
        View.AddView(ViewZone.BODY, new Line(FormatLine.CENTER, " "));

        return true;
    }
}