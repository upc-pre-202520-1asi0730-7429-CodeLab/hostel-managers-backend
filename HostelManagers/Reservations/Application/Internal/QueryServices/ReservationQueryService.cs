using HostelManagers.Reservations.Domain.Model.Aggregates;
using HostelManagers.Reservations.Domain.Model.Queries;
using HostelManagers.Reservations.Domain.Repositories;
using HostelManagers.Reservations.Domain.Services;

namespace HostelManagers.Reservations.Application.Internal.QueryServices;

public class ReservationQueryService (IReservationRepository reservationRepository) : IReservationQueryService
{
    // --- Manejo del Query: Obtener Todas ---
    public async Task<IEnumerable<Reservation>> Handle(GetAllReservationQuery query)
    {
        return await reservationRepository.ListAsync(); 
    }

    // --- Manejo del Query: Obtener por RoomId ---
    public async Task<IEnumerable<Reservation>> Handle(GetReservationByRoomIdQuery query)
    {
        // Llama al método específico del repositorio
        return await reservationRepository.GetByRoomIdAsync(query.RoomId);
    }
    
    // --- Manejo del Query: Obtener por UserId ---
    public async Task<IEnumerable<Reservation>> Handle(GetReservationByUserIdQuery query)
    {
        // Llama al método específico del repositorio
        return await reservationRepository.GetByUserIdAsync(query.UserId);
    }
}