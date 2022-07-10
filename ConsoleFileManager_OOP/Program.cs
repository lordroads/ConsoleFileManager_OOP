using FileManagerOOP.Models;
using FileManagerOOP;
using FileManagerOOP.Interfaces.Load;
using FileManagerOOP.Interfaces.Save;
using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Interfaces.View;
using FileManagerOOP.Interfaces.UserInterface;
using FileManagerOOP.Commands;
using FileManagerOOP.Interfaces.Command;
using FileManagerOOP.Interfaces.Factory;
using FileManagerOOP.Interfaces.Formatter;

bool _run = false;
string nameConfigFile = "config.json";
string pathToStateFile = Path.Combine(Directory.GetCurrentDirectory(), nameConfigFile);

StateActivity<StateConfig> stateActivity;
ProgrammCommandFactory programmCommandFactory;
IUserInterface consoleUserInterface;
IView viewer;
ParserCommand parser;

Init(pathToStateFile);

if (args.Length > 0)
{
    Run(String.Join(' ', args));
}
else
{
    if (stateActivity.CurrentState is not null)
    {
        Run($"view {stateActivity.CurrentState.SelectedPath}");
    }
    else
    {
        Run($"view {Directory.GetCurrentDirectory()}");
    }
    
}

while (!_run)
{
    var result = Run(viewer.ReadValue($"Command > "));

    if (!result.wasSuccessful)
    {
        viewer.ViewWarningLine(new Line { Text = "ЧТо то делается не так!", Format = FileManagerOOP.Enums.FormatLine.CENTER });
    }

    _run = result.shouldQuit;

    stateActivity.Save();

    consoleUserInterface.Refresh();
}

/// <summary>
/// Инициализация зависимостей.
/// </summary>
/// <param name="pathState">Путь до файла конфигурации.</param>
void Init(string pathState)
{
    LoggerExceptionInTxt _logger = new LoggerExceptionInTxt();

    Helper.SetLogger(_logger);

    StateConfig defaultState = new StateConfig()
    {
        CountItemOnPage = 10,
        SelectedPage = 0,
        SelectedPath = Directory.GetCurrentDirectory()
    };

    stateActivity = new StateActivity<StateConfig>(
        defaultState,
        new StateSaveInJson<StateConfig>(pathState, _logger),
        new StateLoadFromJson<StateConfig>(pathState, _logger));

    consoleUserInterface = new ConsoleUserInterface();
    viewer = new ViewController(consoleUserInterface, new FormatterLine());

    parser = new ParserCommand();

    programmCommandFactory = new ProgrammCommandFactory(stateActivity, viewer, _logger);

    stateActivity.Load();
}

(bool wasSuccessful, bool shouldQuit) Run(string line)
{
    (bool wasSuccessful, bool shouldQuit) result;

    var parseLine = parser.Parse(line);

    viewer.Clear();

    ProgrammCommand command = programmCommandFactory.GetCommand(parseLine.CommandName);
    if (command is IParameterisedCommand parameterisedCommand)
    {
        result = parameterisedCommand.SetParameters(parseLine.Args).RunCommand();
    }
    else
    {
        result = command.RunCommand();
    }

    viewer.View();

    return result;
}