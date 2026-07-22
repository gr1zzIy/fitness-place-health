namespace GymApp.Domain.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message = "У вас немає прав для виконання цієї дії.") 
        : base(message)
    {
    }
}