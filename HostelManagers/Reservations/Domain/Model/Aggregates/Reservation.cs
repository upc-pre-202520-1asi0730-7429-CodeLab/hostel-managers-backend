namespace HostelManagers.Reservations.Domain.Model.Aggregates;

public class Reservation
{
    // Propiedades de Identificación (Normalmente solo son 'private set')
    public int Id { get; private set; }
    public string UserId { get; private set; } // O Guid, dependiendo de cómo manejes IDs de usuario
    public int RoomId { get; private set; }    // O Guid, dependiendo de la clave primaria de Room

    // Propiedades de la Reserva
    public DateTime CheckInDate { get; private set; }
    public DateTime CheckOutDate { get; private set; }
    
    // Estado (es recomendable usar un Enum para estados definidos, pero usaremos string por simplicidad inicial)
    public string Status { get; private set; }

    // --- CONSTRUCTORES ---

    /// <summary>
    /// Constructor protegido para uso exclusivo de frameworks de persistencia (ej. Entity Framework Core).
    /// Inicializa con valores por defecto.
    /// </summary>
    protected Reservation()
    {
        // El Id se maneja usualmente por la DB (ValueGeneratedOnAdd)
        // El 0 es un valor por defecto para int no nulo
        Id = 0; 
        UserId = string.Empty;
        RoomId = 0;
        
        // Valores por defecto
        CheckInDate = DateTime.MinValue; // o DateTime.Now
        CheckOutDate = DateTime.MinValue;
        Status = "Pending";
    }

    /// <summary>
    /// Constructor principal para la creación de una nueva reserva.
    /// </summary>
    /// <param name="userId">Identificador del usuario que realiza la reserva.</param>
    /// <param name="roomId">Identificador de la habitación reservada.</param>
    /// <param name="checkInDate">Fecha de entrada.</param>
    /// <param name="checkOutDate">Fecha de salida.</param>
    public Reservation(string userId, int roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        // Asignación de IDs
        UserId = userId;
        RoomId = roomId;
        
        // Asignación de Fechas
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        
        // Estado inicial de una reserva nueva
        Status = "Pending"; 
    }
    
    public void UpdateStatus(string newStatus)
    {
        // Aquí puedes agregar lógica de validación si es necesario
        Status = newStatus;
    }
}