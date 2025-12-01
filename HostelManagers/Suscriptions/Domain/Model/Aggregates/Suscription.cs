using HostelManagers.Suscriptions.Domain.Model.ValueObjets;

namespace HostelManagers.Suscriptions.Domain.Model.Aggregates;

public partial class Suscription
{
    public int Id { get; private set; }
    public Plans Plan { get; private set; }
    
    // 1. Nueva Propiedad para el UserId
    public string UserId { get; private set; } // Lo hacemos 'private set' para que sea inmutable después de la creación
    
    public string PayPalTransactionId { get; set; }
    public Status Statu { get; private set; }
    
    protected Suscription()
    {
        Plan = Plans.Free;
        // Inicialización de la nueva propiedad
        UserId = string.Empty; 
        PayPalTransactionId = string.Empty;
        Statu = Status.NoSuscrito;
    }
    
    // 3. Actualización del Constructor Público con el nuevo parámetro
    public Suscription(string userId, Plans plan, string payPalTransactionId,  Status statu)
    {
        // Asignación del valor de userId
        UserId = userId; 
        Plan = plan;
        PayPalTransactionId = payPalTransactionId;
        Statu = statu;
    }
    
    public Suscription Update(Plans plan, string payPalTransactionId, Status statu)
    {
        // No incluimos 'UserId' en el método Update ya que es un identificador
        // que generalmente no debería cambiar.
        Plan = plan;
        PayPalTransactionId = payPalTransactionId;
        Statu = statu;
        return this;
    }
}