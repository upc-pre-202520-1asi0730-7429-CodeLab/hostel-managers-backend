using HostelManagers.Hotels.Domain.Model.Aggregates;
using HostelManagers.Shared.Domain.Repositories;

namespace HostelManagers.Hotels.Domain.Repositories;

public interface IHotelRepository : IBaseRepository<Hotel>
{
    
}