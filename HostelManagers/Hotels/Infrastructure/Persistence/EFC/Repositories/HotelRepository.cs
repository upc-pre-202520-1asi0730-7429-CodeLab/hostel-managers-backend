using HostelManagers.Hotels.Domain.Model.Aggregates;
using HostelManagers.Hotels.Domain.Repositories;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Configuration;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace HostelManagers.Hotels.Infrastructure.Persistence.EFC.Repositories;

public class HotelRepository (AppDbContext context) : BaseRepository<Hotel>(context), IHotelRepository
{
    
}