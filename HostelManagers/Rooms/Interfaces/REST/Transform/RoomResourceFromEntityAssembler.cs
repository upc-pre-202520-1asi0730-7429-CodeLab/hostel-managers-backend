using HostelManagers.Rooms.Domain.Model.Aggregates;
using HostelManagers.Rooms.Interfaces.REST.Resources;

namespace HostelManagers.Rooms.Interfaces.REST.Transform;

public class RoomResourceFromEntityAssembler
{
    public static RoomResource ToResourceFromEntity(Room entity) => 
        new RoomResource(entity.Id, entity.Type, entity.Price);
}