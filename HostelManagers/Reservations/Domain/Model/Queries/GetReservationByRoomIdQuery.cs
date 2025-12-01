namespace HostelManagers.Reservations.Domain.Model.Queries;

/// <summary>
/// Consulta para obtener todas las reservas asociadas a una habitación específica.
/// </summary>
public record GetReservationByRoomIdQuery(int RoomId);