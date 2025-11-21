using HostelManagers.Rooms.Domain.Model.ValueObjects;

namespace HostelManagers.Rooms.Domain.Model.Commands;

public record UpdateRoomCommand(int Id, Typeroom Type, decimal Price);