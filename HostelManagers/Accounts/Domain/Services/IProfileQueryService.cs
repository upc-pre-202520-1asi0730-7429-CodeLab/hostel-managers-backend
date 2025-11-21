using HostelManagers.Accounts.Domain.Model.Aggregates;
using HostelManagers.Accounts.Domain.Model.Queries;

namespace HostelManagers.Accounts.Domain.Services;

public interface IProfileQueryService
{
    Task<Profiles?> Handle(GetProfileByIdQuery query);
    Task<IEnumerable<Profiles>> Handle(GetAllProfilesQuery query);
}