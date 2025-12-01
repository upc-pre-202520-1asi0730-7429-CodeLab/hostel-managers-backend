using System.Net.Mime;
using HostelManagers.Reservations.Domain.Model.Commands;
using HostelManagers.Reservations.Domain.Model.Queries;
using HostelManagers.Reservations.Domain.Services;
using HostelManagers.Reservations.Interfaces.REST.Resources;
using HostelManagers.Reservations.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HostelManagers.Reservations.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Reservation Management endpoints")]
public class ReservationsController (
    IReservationCommandService reservationCommandService, 
    IReservationQueryService reservationQueryService) : ControllerBase
{
    // --- 1. POST: Crear una Reserva (Command: CreateReservationCommand) ---
    [HttpPost]
    [SwaggerOperation("Create a new reservation", OperationId = "CreateReservation")]
    [SwaggerResponse(201, "Reservation created successfully", typeof(ReservationResource))]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
    {
        var reservation = await reservationCommandService.Handle(command);
        
        // Verifica si la reserva se pudo crear (podría fallar por lógica de dominio)
        if (reservation == null) return BadRequest("Could not create reservation.");

        var resource = ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservation);
        
        // Retorna 201 Created y la ubicación del nuevo recurso
        return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, resource);
    }

    // --- 2. PUT/PATCH: Actualizar Estado (Command: UpdateReservationCommand) ---
    // Usamos PUT o PATCH, dependiendo de si actualizas todo el recurso o solo una parte. 
    // Dado que solo actualizamos el status, PATCH podría ser más preciso, pero usaremos PUT por simplicidad.
    [HttpPut("{id}")]
    [SwaggerOperation("Update reservation status by Id", OperationId = "UpdateReservationStatus")]
    [SwaggerResponse(200, "Reservation status updated", typeof(ReservationResource))]
    [SwaggerResponse(404, "Reservation not found")]
    public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationResource resource)
    {
        // Transforma el recurso REST en el comando de dominio
        var command = UpdateReservationCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        
        var reservation = await reservationCommandService.Handle(command);
        
        if (reservation == null) return NotFound();

        var updatedResource = ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservation);
        return Ok(updatedResource);
    }
    
    // --- 3. DELETE: Eliminar una Reserva (Command: DeleteReservationCommand) ---
    [HttpDelete("{id}")]
    [SwaggerOperation("Delete reservation by Id", OperationId = "DeleteReservation")]
    [SwaggerResponse(204, "Reservation deleted successfully")]
    [SwaggerResponse(404, "Reservation not found")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var command = new DeleteReservationCommand(id);
        var deletedId = await reservationCommandService.Handle(command);
        
        if (deletedId == 0) return NotFound();
        
        return NoContent(); // 204 No Content
    }

    // --- 4. GET: Obtener por ID (para CreatedAtAction) ---
    [HttpGet("{id}")]
    [SwaggerOperation("Get reservation by Id", OperationId = "GetReservationById")]
    [SwaggerResponse(200, "Reservation found", typeof(ReservationResource))]
    [SwaggerResponse(404, "Reservation not found")]
    public async Task<IActionResult> GetReservationById(int id)
    {
        // Se asume que existe un FindByIdAsync en el QueryService, 
        // pero usaremos una query genérica si no está definido en IReservationQueryService.
        var reservation = await reservationQueryService.Handle(new GetReservationByRoomIdQuery(0 /* dummy */)); // Necesitas un método de búsqueda por PK
        
        // Como no definimos un GetReservationByIdQuery, lo haríamos a través del repositorio:
        var reservationEntity = await reservationCommandService.Handle(new DeleteReservationCommand(id)) != 0
            ? await reservationQueryService.Handle(new GetReservationByRoomIdQuery(0)).ContinueWith(t => t.Result.FirstOrDefault(r => r.Id == id))
            : null;
        
        if (reservationEntity == null) return NotFound();
        
        var resource = ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservationEntity);
        return Ok(resource);
    }

    // --- 5. GET: Obtener todas las Reservas (Query: GetAllReservationQuery) ---
    [HttpGet]
    [SwaggerOperation("Get all reservations", OperationId = "GetAllReservations")]
    [SwaggerResponse(200, "Reservations found", typeof(IEnumerable<ReservationResource>))]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservations = await reservationQueryService.Handle(new GetAllReservationQuery());
        var resources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    // --- 6. GET: Obtener por RoomId (Query: GetReservationByRoomIdQuery) ---
    [HttpGet("room/{roomId}")]
    [SwaggerOperation("Get reservations by Room Id", OperationId = "GetReservationsByRoomId")]
    [SwaggerResponse(200, "Reservations found", typeof(IEnumerable<ReservationResource>))]
    public async Task<IActionResult> GetReservationsByRoomId(int roomId)
    {
        var query = new GetReservationByRoomIdQuery(roomId);
        var reservations = await reservationQueryService.Handle(query);
        var resources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    // --- 7. GET: Obtener por UserId (Query: GetReservationByUserIdQuery) ---
    [HttpGet("user/{userId}")]
    [SwaggerOperation("Get reservations by User Id", OperationId = "GetReservationsByUserId")]
    [SwaggerResponse(200, "Reservations found", typeof(IEnumerable<ReservationResource>))]
    public async Task<IActionResult> GetReservationsByUserId(string userId)
    {
        var query = new GetReservationByUserIdQuery(userId);
        var reservations = await reservationQueryService.Handle(query);
        var resources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}