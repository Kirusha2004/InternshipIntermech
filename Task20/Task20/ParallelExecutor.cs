namespace Task20;

public class ParallelExecutor
{
    private readonly IList<Action> _methods = [];

    public bool IsRunning { get; private set; }
    public bool IsCompleted { get; private set; }
    public int Count => _methods.Count;

    public ParallelExecutor AddMethod(Action method)
    {
        if (method != null && !IsRunning)
        {
            _methods.Add(method);
        }
        return this;
    }

    public ParallelExecutor AddMethod(IParallelMethod method)
    {
        if (method != null && !IsRunning)
        {
            _methods.Add(method.Execute);
        }
        return this;
    }

    public Task ExecuteAsync()
    {
        if (_methods.Count == 0)
        {
            throw new InvalidOperationException("Нет методов для выполнения");
        }

        IsRunning = true;
        IsCompleted = false;

        return Task.Run(() =>
        {
            try
            {
                Parallel.Invoke([.. _methods]);
            }
            finally
            {
                IsRunning = false;
                IsCompleted = true;
            }
        });
    }
}
