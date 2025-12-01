using HostelManagers.Shared.Infrastructure.Persistence.EFC.Configuration;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Repositories;
using HostelManagers.Suscriptions.Domain.Model.Aggregates;
using HostelManagers.Suscriptions.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HostelManagers.Suscriptions.Infrastructure.Persistence.EFC.Repositories;

public class SuscriptionRepository (AppDbContext context) : BaseRepository<Suscription>(context), ISuscriptionRepository
{
    public async Task<Suscription?> GetByUserIdAsync(string userId)
    {
        // El DbSet (conjunto de Suscriptions) está disponible a través de la clase base.
        // Buscamos la primera suscripción cuyo UserId coincida con el proporcionado.
        return await context.Set<Suscription>()
            .FirstOrDefaultAsync(s => s.UserId == userId);
    }
}