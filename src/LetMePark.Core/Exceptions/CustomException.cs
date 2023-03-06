namespace LetMePark.Core.Exceptions;

public abstract class CustomException : Exception
{
    protected CustomException(string exceptionMessage) : base(exceptionMessage)
    {

    }
}