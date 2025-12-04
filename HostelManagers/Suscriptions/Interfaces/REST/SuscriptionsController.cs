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
    [SwaggerOperation("Create a suscription", OperationId = "CreateSuscription")]
    [SwaggerResponse(201, "Suscription created", typeof(SuscriptionResource))]
    [SwaggerResponse(400, "Business rule violation")]
    public async Task<IActionResult> CreateSuscription([FromBody] CreateSuscriptionCommand command)
    {
        try 
        {
            var suscription = await suscriptionCommandService.Handle(command);
            var resource = SuscriptionResourceFromEntityAssembler.ToResourceFromEntity(suscription!);
            
            return CreatedAtAction(nameof(GetSuscriptionById), new { id = suscription!.Id }, resource);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
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

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get suscription by Id", OperationId = "GetSuscriptionById")]
    [SwaggerResponse(200, "Suscription found", typeof(SuscriptionResource))]
    [SwaggerResponse(404, "Suscription not found")]
    public async Task<IActionResult> GetSuscriptionById(int id)
    {
        return Ok(); // Placeholder para que compile si no tienes el Query aún
    }
}