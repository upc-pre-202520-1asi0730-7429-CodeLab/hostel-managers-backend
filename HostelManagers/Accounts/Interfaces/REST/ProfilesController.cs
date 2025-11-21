using System.Net.Mime;
using HostelManagers.Accounts.Domain.Model.Commands;
using HostelManagers.Accounts.Domain.Model.Queries;
using HostelManagers.Accounts.Domain.Services;
using HostelManagers.Accounts.Interfaces.REST.Resources;
using HostelManagers.Accounts.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HostelManagers.Accounts.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Profile Management endpoints")]
public class ProfilesController (IProfileCommandService profileCommandService, IProfileQueryService profileQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create a profile", OperationId = "CreateCar")]
    [SwaggerResponse(201, "Profile created", typeof(ProfileResource))]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileCommand command)
    {
        var profile = await profileCommandService.Handle(command);
        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile!);
        return CreatedAtAction(nameof(GetProfileById), new { id = profile!.Id }, new { resource });
    }
    
    [HttpGet]
    [SwaggerOperation("Get all profile", OperationId = "GetAllProfiles")]
    [SwaggerResponse(200, "Profiles found", typeof(IEnumerable<ProfileResource>))]
    public async Task<IActionResult> GetAllProfiles()
    {
        var cars = await profileQueryService.Handle(new GetAllProfilesQuery());
        var resources = cars.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get profile by Id", OperationId = "GetProfileById")]
    [SwaggerResponse(200, "Profile found", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> GetProfileById(int id)
    {
        var profile = await profileQueryService.Handle(new GetProfileByIdQuery(id));
        return profile == null ? NotFound() : Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation("Update a profile", OperationId = "UpdateProfile")]
    [SwaggerResponse(204, "Profile updated")]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> UpdateCar(int id, [FromBody] UpdateProfileCommand command)
    {
        if (id != command.Id) return BadRequest();
        var updated = await profileCommandService.Handle(command);
        if (updated is null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation("Delete a profile", OperationId = "DeleteProfile")]
    [SwaggerResponse(204, "Profile deleted")]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var deleted = await profileCommandService.Handle(new DeleteProfileCommand(id));
        if (!deleted) return NotFound();
        return NoContent();
    }
}