using FileManagerOOP.Enums;

namespace FileManagerOOP.Models;

public class Line
{
    public string Text { get; set; }
    public FormatLine Format { get; set; }

    public Line(FormatLine format = FormatLine.DEFAULT, params string[] text)
    {
        Text = String.Join(' ', text);
        Format = format;
    }
}
