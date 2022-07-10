using FileManagerOOP.Commands;

namespace FileManagerOOP.Interfaces.Factory;

interface IProgrammCommandFactory
{
    ProgrammCommand GetCommand(string command);
}
