using HostelManagers.Rooms.Domain.Model.Aggregates;
using HostelManagers.Rooms.Domain.Repositories;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Configuration;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace HostelManagers.Rooms.Infrastructure.Persistence.EFC.Repositories;

public class RoomRepository (AppDbContext context) : BaseRepository<Room>(context), IRoomRepository
{
    
}