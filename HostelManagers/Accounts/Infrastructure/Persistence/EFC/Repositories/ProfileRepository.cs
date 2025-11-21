using HostelManagers.Accounts.Domain.Model.Aggregates;
using HostelManagers.Accounts.Domain.Repositories;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Configuration;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace HostelManagers.Accounts.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context) : BaseRepository<Profiles>(context), IProfileRepository
{
    
}