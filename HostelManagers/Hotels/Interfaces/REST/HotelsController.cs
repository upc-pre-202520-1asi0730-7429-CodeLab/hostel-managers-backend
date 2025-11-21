using System.Net.Mime;
using HostelManagers.Hotels.Domain.Model.Commands;
using HostelManagers.Hotels.Domain.Model.Queries;
using HostelManagers.Hotels.Domain.Services;
using HostelManagers.Hotels.Interfaces.REST.Resources;
using HostelManagers.Hotels.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HostelManagers.Hotels.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Hotels Management endpoints")]
public class HotelsController (IHotelCommandService hotelCommandService, IHotelQueryService hotelQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create a hotel", OperationId = "CreateHotel")]
    [SwaggerResponse(201, "Hotel created", typeof(HotelResource))]
    public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
    {
        var hotel = await hotelCommandService.Handle(command);
        var resource = HotelResourceFromEntityAssembler.ToResourceFromEntity(hotel!);
        return CreatedAtAction(nameof(GetHotelById), new { id = hotel!.Id }, new { resource });
    }
    
    [HttpGet]
    [SwaggerOperation("Get all hotel", OperationId = "GetAllHotels")]
    [SwaggerResponse(200, "Hotel found", typeof(IEnumerable<HotelResource>))]
    public async Task<IActionResult> GetAllProfiles()
    {
        var hotel = await hotelQueryService.Handle(new GetAllHotelsQuery());
        var resources = hotel.Select(HotelResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get hotel by Id", OperationId = "GetHotelById")]
    [SwaggerResponse(200, "Hotel found", typeof(HotelResource))]
    [SwaggerResponse(404, "Hotel not found")]
    public async Task<IActionResult> GetHotelById(int id)
    {
        var hotel = await hotelQueryService.Handle(new GetHotelByIdQuery(id));
        return hotel == null ? NotFound() : Ok(HotelResourceFromEntityAssembler.ToResourceFromEntity(hotel));
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation("Update a hotel", OperationId = "UpdateHotel")]
    [SwaggerResponse(204, "Hotel updated")]
    [SwaggerResponse(404, "Hotel not found")]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelCommand command)
    {
        if (id != command.Id) return BadRequest();
        var updated = await hotelCommandService.Handle(command);
        if (updated is null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation("Delete a hotel", OperationId = "DeleteHotel")]
    [SwaggerResponse(204, "Hotel deleted")]
    [SwaggerResponse(404, "Hotel not found")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var deleted = await hotelCommandService.Handle(new DeleteHotelCommand(id));
        if (!deleted) return NotFound();
        return NoContent();
    }
}