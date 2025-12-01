using HostelManagers.Reservations.Domain.Model.Aggregates;
using HostelManagers.Reservations.Domain.Model.Queries;

namespace HostelManagers.Reservations.Domain.Services;

public interface IReservationQueryService
{
    // Obtener todas las reservas
    Task<IEnumerable<Reservation>> Handle(GetAllReservationQuery query);

    // Obtener reservas por ID de habitación
    Task<IEnumerable<Reservation>> Handle(GetReservationByRoomIdQuery query);

    // Obtener reservas por ID de usuario
    Task<IEnumerable<Reservation>> Handle(GetReservationByUserIdQuery query);
}