using FileManagerOOP.Interfaces.Load;
using FileManagerOOP.Interfaces.Save;

namespace FileManagerOOP;

public class StateActivity<T>
{
    private readonly ISave<T> _save;
    private readonly ILoad<T> _load;

    public T? CurrentState { get; set; }

    public StateActivity(T defautState, ISave<T> save, ILoad<T> load)
    {
        CurrentState = defautState;
        _save = save;
        _load = load;
    }
    public void Save()
    {
        _save.Save(CurrentState);
    }
    public T Load()
    {
        var state = _load.Load();

        if (state is not null)
        {
            CurrentState = state;
        }

        return CurrentState;
    }
}
