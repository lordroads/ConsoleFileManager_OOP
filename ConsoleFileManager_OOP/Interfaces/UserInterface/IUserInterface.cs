using FileManagerOOP.Models;

namespace FileManagerOOP.Interfaces.UserInterface;

public interface IUserInterface
{
    string ReadValue(string message);
    void WriteMessage(Line message);
    void WriteMessage(string message);
    void WriteWarning(Line message);
    void WriteWarning(string message);
    void Refresh();
    void Clear();
}
