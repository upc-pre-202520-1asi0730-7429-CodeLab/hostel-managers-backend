namespace HostelManagers.Reservations.Domain.Model.Queries;

/// <summary>
/// Consulta para obtener todas las reservas realizadas por un usuario específico.
/// </summary>
public record GetReservationByUserIdQuery(string UserId);