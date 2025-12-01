using HostelManagers.Reservations.Domain.Model.Commands;
using HostelManagers.Reservations.Interfaces.REST.Resources;

namespace HostelManagers.Reservations.Interfaces.REST.Transform;

public class UpdateReservationCommandFromResourceAssembler
{
    public static UpdateReservationCommand ToCommandFromResource(int id, UpdateReservationResource resource) =>
        new UpdateReservationCommand(
            id,              // El ID viene de la ruta (URL) del controlador
            resource.Status  // El nuevo estado viene del cuerpo de la solicitud
        );
}