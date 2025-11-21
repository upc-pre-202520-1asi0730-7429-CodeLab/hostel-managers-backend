using HostelManagers.Suscriptions.Domain.Model.Aggregates;
using HostelManagers.Suscriptions.Domain.Model.Queries;
using HostelManagers.Suscriptions.Domain.Repositories;
using HostelManagers.Suscriptions.Domain.Services;

namespace HostelManagers.Suscriptions.Application.Internal.QueryServices;

public class SuscriptionQueryService (ISuscriptionRepository suscriptionRepository) : ISuscriptionQueryService
{
    public async Task<IEnumerable<Suscription>> Handle(GetAllSuscriptionQuery query)
    {
        return await suscriptionRepository.ListAsync(); 
    }
}