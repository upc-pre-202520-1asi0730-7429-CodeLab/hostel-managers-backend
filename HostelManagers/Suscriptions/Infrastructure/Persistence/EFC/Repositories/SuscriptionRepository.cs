using HostelManagers.Shared.Infrastructure.Persistence.EFC.Configuration;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Repositories;
using HostelManagers.Suscriptions.Domain.Model.Aggregates;
using HostelManagers.Suscriptions.Domain.Repositories;

namespace HostelManagers.Suscriptions.Infrastructure.Persistence.EFC.Repositories;

public class SuscriptionRepository (AppDbContext context) : BaseRepository<Suscription>(context), ISuscriptionRepository
{
    
}