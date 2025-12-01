using HostelManagers.Reservations.Domain.Model.Aggregates;
using HostelManagers.Reservations.Domain.Repositories;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Configuration;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HostelManagers.Reservations.Infrastructure.Persistence.EFC.Repositories;

public class ReservationRepository (AppDbContext context) : BaseRepository<Reservation>(context), IReservationRepository
{
    // --- Implementación de Consultas Específicas ---

    public async Task<IEnumerable<Reservation>> GetByRoomIdAsync(int roomId)
    {
        // Usa el DbSet para filtrar por RoomId
        return await context.Set<Reservation>()
            .Where(r => r.RoomId == roomId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetByUserIdAsync(string userId)
    {
        // Usa el DbSet para filtrar por UserId
        return await context.Set<Reservation>()
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }
}