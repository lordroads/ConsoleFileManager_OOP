using FileManagerOOP.Models;
using System.Text;

namespace FileManagerOOP.Interfaces.Formatter;

public class FormatterLine : IFormatterText
{
    public string Format(Line line)
    {
        switch (line.Format)
        {
            case Enums.FormatLine.CENTER:
                return FormattingCenter(line);
            case Enums.FormatLine.LONG:
                return FormattingLong(line);
            case Enums.FormatLine.RIEGHT:
                return FormattingRieght(line);
            default:
                return FormattingLeft(line);
        }
    }

    private string FormattingLeft(Line line)
    {
        int lengthWhiteSpace = Console.LargestWindowWidth / 2 - line.Text.Length - 2;
        string newText = " " + line.Text + new string(' ', lengthWhiteSpace - 1);

        return newText;
    }

    private string FormattingRieght(Line line)
    {
        int lengthWhiteSpace = Console.LargestWindowWidth / 2 - line.Text.Length - 2;
        string newText = new string(' ', lengthWhiteSpace) + line.Text;

        return newText;
    }

    private string FormattingLong(Line line)
    {
        string[] words = line.Text.Split(' ');
        int space = Console.LargestWindowWidth / 2;
        StringBuilder blockMessage = new StringBuilder();

        for (int i = 0; i < words.Length; i++)
        {
            if (space < 0)
            {
                space = 1;
                break;
            }
            space -= words[i].Length;
        }

        if (words.Length >= 2)
        {
            space = space / (words.Length - 1); 
        }
        else
        {
            space = 1;
        }

        blockMessage.Append(String.Join(new String(' ', space), words));

        return blockMessage.ToString();
    }

    private string FormattingCenter(Line line)
    {
        int lengthWhiteSpace = Console.LargestWindowWidth / 2 - line.Text.Length;
        int space = lengthWhiteSpace / 2;
        string text = line.Text.Replace("\n", "");
        string newText = string.Empty;

        if (space >= 0)
        {
            if (text.Length % 2 == 0)
            {
                newText = new string(' ', space) + line.Text.Replace("\n", "") + new string(' ', space - 2);
            }
            else
            {
                newText = new string(' ', space) + line.Text.Replace("\n", "") + new string(' ', space - 1);

            }
        }
        else
        {
            newText = line.Text.Replace("\n", "");
        }

        

        return newText;
    }
}

