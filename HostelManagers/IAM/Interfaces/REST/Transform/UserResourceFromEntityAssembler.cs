using HostelManagers.IAM.Domain.Model.Aggregates;
using HostelManagers.IAM.Interfaces.REST.Resources;

namespace HostelManagers.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Username);
    }
}