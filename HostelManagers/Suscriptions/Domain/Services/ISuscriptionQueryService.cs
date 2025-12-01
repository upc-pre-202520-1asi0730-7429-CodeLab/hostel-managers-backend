using HostelManagers.Suscriptions.Domain.Model.Aggregates;
using HostelManagers.Suscriptions.Domain.Model.Queries;

namespace HostelManagers.Suscriptions.Domain.Services;

public interface ISuscriptionQueryService
{
    Task<IEnumerable<Suscription>> Handle(GetAllSuscriptionQuery query);
    Task<Suscription?> Handle(GetSuscriptionByUserId query);
}