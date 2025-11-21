using HostelManagers.Rooms.Domain.Model.ValueObjects;

namespace HostelManagers.Rooms.Domain.Model.Commands;

public record CreateRoomCommand(Typeroom Type, decimal Price);