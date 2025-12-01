namespace HostelManagers.Reservations.Interfaces.REST.Resources;

/// <summary>
/// Representa el recurso REST de una Reserva.
/// </summary>
public record ReservationResource(
    int Id, 
    string UserId, 
    int RoomId,
    DateTime CheckInDate,
    DateTime? CheckOutDate, // Usamos DateTime? para permitir nulo
    string Status
);