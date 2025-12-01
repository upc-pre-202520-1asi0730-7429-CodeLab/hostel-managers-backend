using HostelManagers.Reservations.Domain.Model.Aggregates;
using HostelManagers.Reservations.Domain.Model.Commands;

namespace HostelManagers.Reservations.Domain.Services;

public interface IReservationCommandService
{
    // Maneja la creación de una nueva reserva
    Task<Reservation?> Handle(CreateReservationCommand command);

    // Maneja la actualización del estado de una reserva
    Task<Reservation?> Handle(UpdateReservationCommand command);

    // Maneja la eliminación de una reserva
    Task<int> Handle(DeleteReservationCommand command);
}