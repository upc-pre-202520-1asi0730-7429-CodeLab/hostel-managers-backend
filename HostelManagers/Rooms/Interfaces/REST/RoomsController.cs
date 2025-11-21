using System.Net.Mime;
using HostelManagers.Rooms.Domain.Model.Commands;
using HostelManagers.Rooms.Domain.Model.Queries;
using HostelManagers.Rooms.Domain.Services;
using HostelManagers.Rooms.Interfaces.REST.Resources;
using HostelManagers.Rooms.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HostelManagers.Rooms.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Rooms Management endpoints")]
public class RoomsController (IRoomCommandService roomCommandService, IRoomQueryService roomQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create a room", OperationId = "CreateRoom")]
    [SwaggerResponse(201, "Room created", typeof(RoomResource))]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomCommand command)
    {
        var room = await roomCommandService.Handle(command);
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room!);
        return CreatedAtAction(nameof(GetRoomById), new { id = room!.Id }, new { resource });
    }
    
    [HttpGet]
    [SwaggerOperation("Get all room", OperationId = "GetAllRooms")]
    [SwaggerResponse(200, "Room found", typeof(IEnumerable<RoomResource>))]
    public async Task<IActionResult> GetAllRooms()
    {
        var room = await roomQueryService.Handle(new GetAllRoomsQuery());
        var resources = room.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get room by Id", OperationId = "GetRoomsById")]
    [SwaggerResponse(200, "Room found", typeof(RoomResource))]
    [SwaggerResponse(404, "Room not found")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        var hotel = await roomQueryService.Handle(new GetRoomByIdQuery(id));
        return hotel == null ? NotFound() : Ok(RoomResourceFromEntityAssembler.ToResourceFromEntity(hotel));
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation("Update a room", OperationId = "UpdateRoom")]
    [SwaggerResponse(204, "Room updated")]
    [SwaggerResponse(404, "Room not found")]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] UpdateRoomCommand command)
    {
        if (id != command.Id) return BadRequest();
        var updated = await roomCommandService.Handle(command);
        if (updated is null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation("Delete a room", OperationId = "DeleteRoom")]
    [SwaggerResponse(204, "Room deleted")]
    [SwaggerResponse(404, "Room not found")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var deleted = await roomCommandService.Handle(new DeleteRoomCommand(id));
        if (!deleted) return NotFound();
        return NoContent();
    }
}