using HostelManagers.Suscriptions.Domain.Model.ValueObjets;

namespace HostelManagers.Suscriptions.Domain.Model.Aggregates;

public partial class Suscription
{
    public int Id { get; private set; }
    public Plans Plan { get; private set; }
    
    public string PayPalTransactionId { get; set; }
    public Status Statu { get; private set; }
    
    protected Suscription()
    {
        Plan = Plans.Free;
        PayPalTransactionId = string.Empty;
        Statu = Status.NoSuscrito;
    }
    
    public Suscription(Plans plan, string payPalTransactionId,  Status statu)
    {
        Plan = plan;
        PayPalTransactionId = payPalTransactionId;
        Statu = statu;
    }
    
    public Suscription Update(Plans plan, string payPalTransactionId, Status statu)
    {
        Plan = plan;
        PayPalTransactionId = payPalTransactionId;
        Statu = statu;
        return this;
    }
}