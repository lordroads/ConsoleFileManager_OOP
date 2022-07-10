using FileManagerOOP.Models;

namespace FileManagerOOP.Interfaces.UserInterface;

public class ConsoleUserInterface : IUserInterface
{
    public void Clear()
    {
        Console.Clear();
    }

    public string ReadValue(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(message);
        return Console.ReadLine().Replace("\"", "");
    }

    public void Refresh()
    {
        Console.ResetColor();
    }

    public void WriteMessage(Line message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(message.Text);
    }

    public void WriteMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(message);
    }

    public void WriteWarning(Line message)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(message.Text);
    }

    public void WriteWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(message);
    }
}