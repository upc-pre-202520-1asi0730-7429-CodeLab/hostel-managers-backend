namespace HostelManagers.Reservations.Domain.Model.Commands;

public record UpdateReservationCommand(
    int Id,             // El identificador único de la reserva a actualizar
    string Status       // El nuevo estado (ej: "Confirmed", "Cancelled", "Completed")
);