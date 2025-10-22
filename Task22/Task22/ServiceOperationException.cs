namespace Task22;
public class ServiceOperationException : Exception
{
    public ServiceOperationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
