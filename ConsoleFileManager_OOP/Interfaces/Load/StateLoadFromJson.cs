using FileManagerOOP.Interfaces.Logger;
using FileManagerOOP.Models;
using System.Diagnostics;
using System.Text.Json;

namespace FileManagerOOP.Interfaces.Load;

public class StateLoadFromJson<T> : ILoad<T>
{
    private readonly string _path;
    private readonly ILogger _logger;

    public StateLoadFromJson(string path, ILogger logger)
    {
        _path = path;
        _logger = logger;
    }
    public T? Load()
    {
        try
        {
            string jsonState = File.ReadAllText(_path);
            T loadState = JsonSerializer.Deserialize<T>(jsonState);

            return loadState;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            _logger.Log(ex);
            return default(T);
        }
    }
}
