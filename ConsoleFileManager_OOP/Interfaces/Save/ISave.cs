using FileManagerOOP.Models;

namespace FileManagerOOP.Interfaces.Save;

public interface ISave<T>
{
    bool Save(T state);
}
