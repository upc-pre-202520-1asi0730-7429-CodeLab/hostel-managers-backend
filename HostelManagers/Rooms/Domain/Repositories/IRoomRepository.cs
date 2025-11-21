using HostelManagers.Rooms.Domain.Model.Aggregates;
using HostelManagers.Shared.Domain.Repositories;

namespace HostelManagers.Rooms.Domain.Repositories;

public interface IRoomRepository : IBaseRepository<Room>
{
    
}