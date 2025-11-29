namespace HostelManagers.Rooms.Interfaces.REST.Resources;

public record RoomResource(int Id, string Imagen, string Type, decimal Price, int HotelId);