namespace GymApp.Application.Gyms.Commands.CreateGym;

public record CreateGymCommand(
    string Name, 
    string? Description, 
    string TimeZone);