using HostelManagers.Reservations.Domain.Model.Aggregates;

namespace HostelManagers.Reservations.Domain.Repositories;

public interface IReservationRepository
{
    // --- Comandos (Escritura) ---
    
    // Añadir una nueva reserva
    Task AddAsync(Reservation reservation);

    // Actualizar (EF Core rastrea cambios, pero es bueno tener un método de obtención para el flujo)
    Task<Reservation?> FindByIdAsync(int id);
    
    // Eliminar
    void Remove(Reservation reservation);

    // --- Consultas (Lectura) ---
    
    // Obtener todas las reservas
    Task<IEnumerable<Reservation>> ListAsync(); 
    
    // Obtener reservas por ID de habitación
    Task<IEnumerable<Reservation>> GetByRoomIdAsync(int roomId);
    
    // Obtener reservas por ID de usuario
    Task<IEnumerable<Reservation>> GetByUserIdAsync(string userId);
}