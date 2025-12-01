using HostelManagers.Reservations.Domain.Model.Aggregates;
using HostelManagers.Reservations.Interfaces.REST.Resources;

namespace HostelManagers.Reservations.Interfaces.REST.Transform;

public class ReservationResourceFromEntityAssembler
{
    /// <summary>
    /// Transforma una entidad Reservation en un recurso ReservationResource.
    /// </summary>
    /// <param name="entity">La entidad Reservation de dominio.</param>
    /// <returns>El recurso REST de la Reserva.</returns>
    public static ReservationResource ToResourceFromEntity(Reservation entity) =>
        new ReservationResource(
            entity.Id, 
            entity.UserId, 
            entity.RoomId, 
            entity.CheckInDate, 
            entity.CheckOutDate, // Mapea el valor de tipo DateTime?
            entity.Status
        );
}