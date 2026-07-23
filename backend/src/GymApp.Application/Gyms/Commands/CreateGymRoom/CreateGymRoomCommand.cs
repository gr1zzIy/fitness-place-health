namespace GymApp.Application.Gyms.Commands.CreateGymRoom;

public record CreateGymRoomCommand(
    Guid GymBranchId, 
    string Name, 
    int Capacity);