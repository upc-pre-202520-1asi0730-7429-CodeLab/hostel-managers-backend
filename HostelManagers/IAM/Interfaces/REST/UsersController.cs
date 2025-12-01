using System.Net.Mime;
using HostelManagers.IAM.Domain.Model.Queries;
using HostelManagers.IAM.Domain.Services;
using HostelManagers.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using HostelManagers.IAM.Interfaces.REST.Resources;
using HostelManagers.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HostelManagers.IAM.Interfaces.REST;

/**
 * <summary>
 *     The user's controller
 * </summary>
 * <remarks>
 *     This class is used to handle user requests
 * </remarks>
 */
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User endpoints")]
public class UsersController(IUserQueryService userQueryService) : ControllerBase
{
    /**
     * <summary>
     *     Get user by id endpoint. It allows to get a user by id
     * </summary>
     * <param name="id">The user id</param>
     * <returns>The user resource</returns>
     */
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a user by its id",
        Description = "Get a user by its id",
        OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was found", typeof(UserResource))]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }

    /**
     * <summary>
     *     Get all users' endpoint. It allows getting all users
     * </summary>
     * <returns>The user resources</returns>
     */
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Get all users",
        OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "The users were found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
    
    
    [HttpGet("{userId}/role")]
    [SwaggerOperation("Get user role by Id", OperationId = "GetUserRoleById")]
    public async Task<IActionResult> GetUserRoleById(int userId)
    {
        var query = new GetUserByIdQuery(userId);
        var user = await userQueryService.Handle(query);

        if (user == null)
        {
            return NotFound($"User with ID {userId} not found.");
        }

        // ⭐ Aquí es donde se usa el transformador para obtener solo el rol
        var resource = UserRoleResourceFromEntityAssembler.ToResourceFromEntity(user); 

        return Ok(resource);
    }
}