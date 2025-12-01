namespace HostelManagers.Reservations.Domain.Model.Commands;

public record CreateReservationCommand(
    string UserId,          // ID del usuario que realiza la reserva
    int RoomId,             // ID de la habitación reservada
    DateTime CheckInDate,   // Fecha de entrada
    DateTime CheckOutDate   // Fecha de salida
);