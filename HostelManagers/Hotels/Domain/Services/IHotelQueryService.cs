using HostelManagers.Hotels.Domain.Model.Aggregates;
using HostelManagers.Hotels.Domain.Model.Queries;

namespace HostelManagers.Hotels.Domain.Services;

public interface IHotelQueryService
{
    Task<Hotel?> Handle(GetHotelByIdQuery query); 
    Task<IEnumerable<Hotel>> Handle(GetAllHotelsQuery query);
}