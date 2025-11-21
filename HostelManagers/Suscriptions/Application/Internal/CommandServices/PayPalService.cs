using HostelManagers.Suscriptions.Domain.Services;

namespace HostelManagers.Suscriptions.Application.Internal.CommandServices;

public class PayPalService : IPayPalService
{
    public async Task<bool> IsPaymentApprovedAsync(string paypalTransactionId)
    {
        return true;
    }
}