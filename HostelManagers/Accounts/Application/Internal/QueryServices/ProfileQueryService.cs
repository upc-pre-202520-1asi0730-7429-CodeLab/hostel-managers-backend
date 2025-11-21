using HostelManagers.Accounts.Domain.Model.Aggregates;
using HostelManagers.Accounts.Domain.Model.Queries;
using HostelManagers.Accounts.Domain.Repositories;
using HostelManagers.Accounts.Domain.Services;

namespace HostelManagers.Accounts.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    public async Task<Profiles?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Profiles>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }
}