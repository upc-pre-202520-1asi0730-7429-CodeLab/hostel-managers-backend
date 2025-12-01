using System.ComponentModel.DataAnnotations;

namespace HostelManagers.Reservations.Interfaces.REST.Resources;

/// <summary>
/// Recurso para actualizar el estado de una Reserva existente.
/// </summary>
public record UpdateReservationResource(
    // El Id no se incluye aquí porque normalmente viene de la ruta (URL)
    
    [Required]
    string Status // El nuevo estado (ej: "Confirmed", "Cancelled")
);