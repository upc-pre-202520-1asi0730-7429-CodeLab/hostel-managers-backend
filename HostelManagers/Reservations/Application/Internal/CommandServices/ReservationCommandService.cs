using HostelManagers.Reservations.Domain.Model.Aggregates;
using HostelManagers.Reservations.Domain.Model.Commands;
using HostelManagers.Reservations.Domain.Repositories;
using HostelManagers.Reservations.Domain.Services;
using HostelManagers.Shared.Domain.Repositories;

namespace HostelManagers.Reservations.Application.Internal.CommandServices;

public class ReservationCommandService : IReservationCommandService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Uso del constructor primario de C# 12 para inyección de dependencias
    public ReservationCommandService(
        IReservationRepository reservationRepository,
        IUnitOfWork unitOfWork)
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }

    // --- Manejo del Comando de Creación ---
    public async Task<Reservation?> Handle(CreateReservationCommand command)
    {
        // 1. Crear la entidad de dominio
        var reservation = new Reservation(
            command.UserId,
            command.RoomId,
            command.CheckInDate,
            command.CheckOutDate
        );

        // 2. Persistir
        await _reservationRepository.AddAsync(reservation);
        await _unitOfWork.CompleteAsync(); 
        
        return reservation;
    }
    
    // --- Manejo del Comando de Actualización de Estado ---
    public async Task<Reservation?> Handle(UpdateReservationCommand command)
    {
        // 1. Encontrar la reserva existente
        var reservation = await _reservationRepository.FindByIdAsync(command.Id);

        if (reservation == null) return null;

        // 2. Aplicar la lógica de negocio para la actualización
        // Nota: Si usaras un Enum, aquí convertirías el string 'command.Status' al Enum
        // Por simplicidad, asumimos un método UpdateStatus en la entidad.
        reservation.UpdateStatus(command.Status);

        // 3. Persistir (EF Core rastrea el cambio, solo necesitamos guardar)
        await _unitOfWork.CompleteAsync();

        return reservation;
    }

    // --- Manejo del Comando de Eliminación ---
    public async Task<int> Handle(DeleteReservationCommand command)
    {
        // 1. Encontrar la reserva existente
        var reservation = await _reservationRepository.FindByIdAsync(command.Id);

        if (reservation == null) return 0; // 0 significa que no se eliminó nada

        // 2. Eliminar del contexto
        _reservationRepository.Remove(reservation);
        await _unitOfWork.CompleteAsync();

        return command.Id; // Retorna el ID de la reserva eliminada
    }
}