
namespace TaskTracker.Infastructore.Exceptions;

public class NotFoundUserBtEmailException : Exception
{
    public NotFoundUserBtEmailException(string message) : base(message) { }
}
