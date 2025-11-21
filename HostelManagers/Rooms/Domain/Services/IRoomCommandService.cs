using HostelManagers.Rooms.Domain.Model.Aggregates;
using HostelManagers.Rooms.Domain.Model.Commands;

namespace HostelManagers.Rooms.Domain.Services;

public interface IRoomCommandService
{
    Task<Room?> Handle(CreateRoomCommand command); 
    Task<Room?> Handle(UpdateRoomCommand command);
    Task<bool> Handle(DeleteRoomCommand command);
}