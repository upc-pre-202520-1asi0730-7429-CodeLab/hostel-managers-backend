using HostelManagers.Hotels.Domain.Model.Aggregates;
using HostelManagers.Hotels.Interfaces.REST.Resources;

namespace HostelManagers.Hotels.Interfaces.REST.Transform;

public class HotelResourceFromEntityAssembler
{
    public static HotelResource ToResourceFromEntity(Hotel entity) => 
        new HotelResource(entity.Id, entity.Name, entity.Images, entity.Address, entity.Phone);    
}