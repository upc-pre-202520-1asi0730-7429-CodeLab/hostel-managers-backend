namespace HostelManagers.Suscriptions.Domain.Services;

public interface IPayPalService  // PayPal Sandbox
{
    Task<bool> IsPaymentApprovedAsync(string paypalTransactionId);
}