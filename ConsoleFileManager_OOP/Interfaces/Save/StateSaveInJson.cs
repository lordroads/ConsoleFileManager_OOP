using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Models;
using System.Diagnostics;
using System.Text.Json;

namespace FileManagerOOP.Interfaces.Save;

public class StateSaveInJson<T> : ISave<T>
{
    private readonly string _path;
    private readonly ILogger _logger;

    public StateSaveInJson(string path, ILogger logger)
    {
        _path = path;
        _logger = logger;
    }
    public bool Save(T state)
    {
        try
        {
            string jsonState = JsonSerializer.Serialize(state);
            File.WriteAllText(_path, jsonState);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            _logger.Log(ex);
            return false;
        }
    }
}
