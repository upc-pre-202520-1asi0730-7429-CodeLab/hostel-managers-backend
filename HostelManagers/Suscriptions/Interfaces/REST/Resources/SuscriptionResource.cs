using HostelManagers.Suscriptions.Domain.Model.ValueObjets;

namespace HostelManagers.Suscriptions.Interfaces.REST.Resources;

public record SuscriptionResource(int Id, Plans Plan, string PayPalTransactionId, Status Statu);