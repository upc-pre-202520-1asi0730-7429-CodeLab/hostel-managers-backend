using System.Net.Mime;
using HostelManagers.Suscriptions.Domain.Model.Commands;
using HostelManagers.Suscriptions.Domain.Model.Queries;
using HostelManagers.Suscriptions.Domain.Services;
using HostelManagers.Suscriptions.Interfaces.REST.Resources;
using HostelManagers.Suscriptions.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HostelManagers.Suscriptions.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Suscription Management endpoints")]
public class SuscriptionsController (ISuscriptionCommandService suscriptionCommandService, ISuscriptionQueryService suscriptionQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create a profile", OperationId = "CreateCar")]
    [SwaggerResponse(201, "Profile created", typeof(SuscriptionResource))]
    public async Task<IActionResult> CreateSuscription([FromBody] CreateSuscriptionCommand command)
    {
        var suscription = await suscriptionCommandService.Handle(command);
        var resource = SuscriptionResourceFromEntityAssembler.ToResourceFromEntity(suscription!);
        return CreatedAtAction(nameof(GetAllSuscriptions), new { id = suscription!.Id }, new { resource });
    }
    
    [HttpGet]
    [SwaggerOperation("Get all suscription", OperationId = "GetAllSuscriptions")]
    [SwaggerResponse(200, "Suscription found", typeof(IEnumerable<SuscriptionResource>))]
    public async Task<IActionResult> GetAllSuscriptions()
    {
        var suscription = await suscriptionQueryService.Handle(new GetAllSuscriptionQuery());
        var resources = suscription.Select(SuscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}