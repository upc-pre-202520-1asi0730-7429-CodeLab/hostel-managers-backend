namespace HostelManagers.Rooms.Domain.Model.Commands;

public record CreateRoomCommand(string Imagen, string Type, decimal Price, int HotelId);