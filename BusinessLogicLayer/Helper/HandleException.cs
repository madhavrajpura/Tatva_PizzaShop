namespace BusinessLogicLayer.Helper;

public class HandleException : Exception
{
    public HandleException(string message) : base(message)
    {
    }

    public HandleException(string message, Exception innerException) : base(message, innerException)
    {

    }
}