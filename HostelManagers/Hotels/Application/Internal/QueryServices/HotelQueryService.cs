using HostelManagers.Hotels.Domain.Model.Aggregates;
using HostelManagers.Hotels.Domain.Model.Queries;
using HostelManagers.Hotels.Domain.Repositories;
using HostelManagers.Hotels.Domain.Services;

namespace HostelManagers.Hotels.Application.Internal.QueryServices;

public class HotelQueryService (IHotelRepository hotelRepository) :IHotelQueryService
{
    public async Task<Hotel?> Handle(GetHotelByIdQuery query)
    {
        return await hotelRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Hotel>> Handle(GetAllHotelsQuery query)
    {
        return await hotelRepository.ListAsync();
    }
}