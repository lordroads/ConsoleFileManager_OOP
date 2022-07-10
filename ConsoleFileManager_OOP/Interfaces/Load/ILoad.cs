using FileManagerOOP.Models;

namespace FileManagerOOP.Interfaces.Load;

public interface ILoad<T>
{
    T? Load();
}
