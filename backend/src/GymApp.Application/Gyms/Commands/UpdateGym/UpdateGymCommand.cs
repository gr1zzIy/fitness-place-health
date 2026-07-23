namespace GymApp.Application.Gyms.Commands.UpdateGym;

public record UpdateGymCommand(
    Guid Id, 
    string Name, 
    string? Description, 
    string TimeZone, 
    bool IsActive);