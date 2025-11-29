namespace HostelManagers.Rooms.Domain.Model.Commands;

public record UpdateRoomCommand(int Id, string Imagen, string Type, decimal Price, int HotelId);