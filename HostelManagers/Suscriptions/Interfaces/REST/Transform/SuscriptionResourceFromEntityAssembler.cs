using HostelManagers.Suscriptions.Domain.Model.Aggregates;
using HostelManagers.Suscriptions.Interfaces.REST.Resources;

namespace HostelManagers.Suscriptions.Interfaces.REST.Transform;

public class SuscriptionResourceFromEntityAssembler
{
    public static SuscriptionResource ToResourceFromEntity(Suscription entity) =>
        new SuscriptionResource(
            entity.Id, 
            entity.UserId, // <-- ¡Añadido el mapeo de UserId!
            entity.Plan, 
            entity.PayPalTransactionId, 
            entity.Statu
        );
}