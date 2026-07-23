namespace GymApp.Application.Gyms.Queries.DTOs;

public record GymBranchDto(
    Guid Id, 
    string Name, 
    string Address, 
    string? PhoneNumber,
    bool IsActive, 
    List<GymRoomDto> Rooms);