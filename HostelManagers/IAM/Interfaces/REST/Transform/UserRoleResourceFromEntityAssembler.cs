using HostelManagers.IAM.Domain.Model.Aggregates;
using HostelManagers.IAM.Interfaces.REST.Resources;

namespace HostelManagers.IAM.Interfaces.REST.Transform;

public class UserRoleResourceFromEntityAssembler
{
    public static UserRoleResource ToResourceFromEntity(User entity) =>
        new UserRoleResource(
            entity.Id,
            entity.Roles // Mapea la propiedad Roles de la entidad al campo Role del recurso
        );
}