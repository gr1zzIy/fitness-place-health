namespace GymApp.Application.Gyms.Queries.DTOs;

public record GymRoomDto(
    Guid Id, 
    string Name, 
    int Capacity, 
    bool IsActive);
