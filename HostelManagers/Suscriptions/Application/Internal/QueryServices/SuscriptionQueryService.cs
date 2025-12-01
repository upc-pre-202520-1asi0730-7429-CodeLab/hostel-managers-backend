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
    
    // Método corregido
    public async Task<Suscription?> Handle(GetSuscriptionByUserId query)
    {
        // ⭐ Corregido: Usar GetByUserIdAsync en lugar de FindByIdAsync
        return await suscriptionRepository.GetByUserIdAsync(query.UserId);
    }
}