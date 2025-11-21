using HostelManagers.Rooms.Domain.Model.ValueObjects;

namespace HostelManagers.Rooms.Interfaces.REST.Resources;

public record RoomResource(int Id, Typeroom Type, decimal Price);