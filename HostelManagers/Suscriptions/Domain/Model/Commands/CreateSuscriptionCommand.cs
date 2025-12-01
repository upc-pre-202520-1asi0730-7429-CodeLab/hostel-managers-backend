using HostelManagers.Suscriptions.Domain.Model.ValueObjets;

namespace HostelManagers.Suscriptions.Domain.Model.Commands;

public record CreateSuscriptionCommand(
    string UserId, // <-- ¡Añadido!
    Plans Plan, 
    string PayPalTransactionId, 
    Status Statu
);  