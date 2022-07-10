using FileManagerOOP.Models;
using FileManagerOOP.Interfaces.UserInterface;
using FileManagerOOP.Enums;
using System.Text;
using FileManagerOOP.Interfaces.Formatter;

namespace FileManagerOOP.Interfaces.View;
public class ViewController : IView
{
    private readonly IUserInterface _interface;
    private readonly IFormatterText _formatter;

    char[] UI = new char[] { '\u250C', '\u2510', '\u2514', '\u2518', '\u251C', '\u2524', '\u2500', '\u252C', '\u2534', '\u2502' }; // 0┌ 1┐ 2└ 3┘ 4├ 5┤ 6─ 7┬ 8┴ 9│
    int width = Console.LargestWindowWidth / 2;

    StringBuilder viewConsole = new StringBuilder();

    StringBuilder headerLines = new StringBuilder();
    StringBuilder bodyLines = new StringBuilder();
    StringBuilder footerLines = new StringBuilder();

    StringBuilder backup = new StringBuilder();

    string[] _tempMessage = new string[0];

    public ViewController(IUserInterface userInterface, IFormatterText formatter)
    {
        _interface = userInterface;
        _formatter = formatter;
    }

    public void AddView(ViewZone viewZone, Line line)
    {
        switch (viewZone)
        {
            case ViewZone.HEADER:
                AddHeader(line);
                break;
            case ViewZone.BODY:
                AddBody(line);
                break;
            case ViewZone.FOOTER:
                AddFooter(line);
                break;
        }
    }
    public void View()
    {
        if (viewConsole == null)
        {
            viewConsole = new StringBuilder();
        }

        var warningLines = viewConsole.ToString().Split('\n');
        if (warningLines.Length > 0)
        {
            foreach (var warningLine in warningLines)
            {
                if (!String.IsNullOrWhiteSpace(warningLine))
                {
                    Line line = new Line()
                    {
                        Text = "Warning: " + warningLine,
                        Format = FormatLine.CENTER
                    };

                    string formatString = _formatter.Format(line);

                    _interface.WriteWarning(formatString + "\n");
                }
            }
        }


        if (headerLines.Length > 0)
        {
            _interface.WriteMessage(headerLines.ToString());
        }
        else
        {
            AddHeader(new Line(FormatLine.LONG, DateTime.Now.ToString("HH:mm:ss_dd.MM.yyyy")));
            _interface.WriteMessage(headerLines.ToString());
        }

        /* Start line body */
        _interface.WriteMessage(UI[0].ToString());
        _interface.WriteMessage(new String(UI[6], width - 2));
        _interface.WriteMessage(UI[1].ToString());
        _interface.WriteMessage("\n");

        /*BODY*/
        _interface.WriteMessage(bodyLines.ToString());

        /* End line body */
        _interface.WriteMessage(UI[4].ToString());
        _interface.WriteMessage(new String(UI[6], width - 2));
        _interface.WriteMessage(UI[5].ToString());
        _interface.WriteMessage("\n");

        _interface.WriteMessage(footerLines.ToString());

        /* End line footer */
        _interface.WriteMessage(UI[2].ToString());
        _interface.WriteMessage(new String(UI[6], width - 2));
        _interface.WriteMessage(UI[3].ToString() + "\n");

        if (_tempMessage.Length > 0)
        {
            for (int i = 0; i < _tempMessage.Length; i++)
            {
                viewConsole.Replace(_tempMessage[i] + "\n", string.Empty);
            }

            _tempMessage = Array.Empty<string>();
        }

        backup = viewConsole;
    }
    public void ViewWarningLine(params Line[] line)
    {
        viewConsole = backup;
        _tempMessage = new string[line.Length];

        for (int i = 0; i < line.Length; i++)
        {
            _tempMessage[i] = line[i].Text;
            viewConsole.Append(line[i].Text);
            viewConsole.Append('\n');
        }
    }
    public void Clear()
    {
        Console.Clear();
        viewConsole = new StringBuilder();
        headerLines = new StringBuilder();
        bodyLines = new StringBuilder();
        footerLines = new StringBuilder();
    }
    public string ReadValue(string message)
    {
        return _interface.ReadValue(message);
    }
    private void AddHeader(Line line)
    {
        string headerLine = _formatter.Format(line);

        headerLines.Append(headerLine + "\n");
    }
    private void AddBody(Line line)
    {
        string bodyLine = _formatter.Format(line);
        string borderAndBody = UI[9].ToString() + bodyLine + UI[9].ToString() + "\n";

        bodyLines.Append(borderAndBody);
    }
    private void AddFooter(Line line)
    {
        string footerLine = _formatter.Format(line);
        string borderAndFooter = UI[9].ToString() + footerLine + UI[9].ToString() + "\n";

        footerLines.Append(borderAndFooter);
    }
}



