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
    
    // 🆕 Nuevo Endpoint para obtener Suscripción por UserId
    [HttpGet("{userId}")] // Define la ruta para incluir el userId
    [SwaggerOperation("Get suscription by UserId", OperationId = "GetSuscriptionByUserId")]
    [SwaggerResponse(200, "Suscription found", typeof(SuscriptionResource))]
    [SwaggerResponse(404, "Suscription not found")]
    public async Task<IActionResult> GetSuscriptionByUserId(string userId)
    {
        // 1. Crea la query con el parámetro recibido
        var query = new GetSuscriptionByUserId(userId);
        
        // 2. Llama al servicio de consulta
        var suscription = await suscriptionQueryService.Handle(query);
        
        // 3. Verifica si se encontró la suscripción
        if (suscription == null) return NotFound();
        
        // 4. Mapea la entidad al recurso y retorna 200 OK
        var resource = SuscriptionResourceFromEntityAssembler.ToResourceFromEntity(suscription);
        return Ok(resource);
    }
}