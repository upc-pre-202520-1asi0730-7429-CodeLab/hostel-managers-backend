using HostelManagers.Suscriptions.Domain.Model.ValueObjets;

namespace HostelManagers.Suscriptions.Interfaces.REST.Resources;

public record SuscriptionResource(
    int Id, 
    string UserId, // <-- ¡Añadido!
    Plans Plan, 
    string PayPalTransactionId, 
    Status Statu
);