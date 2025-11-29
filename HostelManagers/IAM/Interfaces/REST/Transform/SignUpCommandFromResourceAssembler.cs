using HostelManagers.IAM.Domain.Model.Commands;
using HostelManagers.IAM.Interfaces.REST.Resources;

namespace HostelManagers.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password, resource.Names, resource.Roles);
    }
}