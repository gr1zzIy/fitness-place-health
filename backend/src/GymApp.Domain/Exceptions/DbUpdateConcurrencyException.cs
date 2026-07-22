namespace GymApp.Domain.Exceptions;

public class DbUpdateConcurrencyException : Exception
{
    public DbUpdateConcurrencyException(string message = "Конфлікт оновлення даних. Дані були змінені іншим користувачем.")
        : base(message)
    {
    }
}