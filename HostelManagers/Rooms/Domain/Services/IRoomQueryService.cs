using HostelManagers.Rooms.Domain.Model.Aggregates;
using HostelManagers.Rooms.Domain.Model.Queries;

namespace HostelManagers.Rooms.Domain.Services;

public interface IRoomQueryService
{
    Task<Room?> Handle(GetRoomByIdQuery query);
    Task<IEnumerable<Room>> Handle(GetAllRoomsQuery query);
}