using HostelManagers.Accounts.Domain.Model.Aggregates;
using HostelManagers.Accounts.Interfaces.REST.Resources;

namespace HostelManagers.Accounts.Interfaces.REST.Transform;

public class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profiles entity) =>
        new ProfileResource(entity.Id, entity.Names, entity.Roles, entity.UserId, entity.SuscriptionId); 
}