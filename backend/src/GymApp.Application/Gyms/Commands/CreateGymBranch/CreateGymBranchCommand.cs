namespace GymApp.Application.Gyms.Commands.CreateGymBranch;

public record CreateGymBranchCommand(
    Guid GymId, 
    string Name, 
    string Address, 
    string? PhoneNumber);