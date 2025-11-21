using HostelManagers.Rooms.Domain.Model.Aggregates;
using HostelManagers.Rooms.Domain.Model.Queries;
using HostelManagers.Rooms.Domain.Repositories;
using HostelManagers.Rooms.Domain.Services;

namespace HostelManagers.Rooms.Application.Internal.QueryServices;

public class RoomQueryService (IRoomRepository roomRepository) : IRoomQueryService
{
    public async Task<Room?> Handle(GetRoomByIdQuery query)
    {
        return await roomRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Room>> Handle(GetAllRoomsQuery query)
    {
        return await roomRepository.ListAsync();
    }
}