using FileManagerOOP.Commands;

namespace FileManagerOOP.Interfaces.Command;

/// <summary>
/// Для определения команд с параметрами.
/// </summary>
interface IParameterisedCommand
{
    ProgrammCommand SetParameters(params string[] parameters);
    bool GetParameters();
}
