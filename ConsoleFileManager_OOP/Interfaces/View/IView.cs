using FileManagerOOP.Enums;
using FileManagerOOP.Models;

namespace FileManagerOOP.Interfaces.View;

public interface IView
{
    public void AddView(ViewZone viewZone, Line line);
    void View();
    void ViewWarningLine(params Line[] line);
    void Clear();
    string ReadValue(string value);
}
