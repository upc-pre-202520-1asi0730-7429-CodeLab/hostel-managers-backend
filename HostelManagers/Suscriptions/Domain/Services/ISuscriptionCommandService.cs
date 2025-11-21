using HostelManagers.Suscriptions.Domain.Model.Aggregates;
using HostelManagers.Suscriptions.Domain.Model.Commands;

namespace HostelManagers.Suscriptions.Domain.Services;

public interface ISuscriptionCommandService
{
    Task<Suscription?> Handle(CreateSuscriptionCommand command);
}