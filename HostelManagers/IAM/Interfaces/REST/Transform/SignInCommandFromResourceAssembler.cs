using HostelManagers.IAM.Domain.Model.Commands;
using HostelManagers.IAM.Interfaces.REST.Resources;

namespace HostelManagers.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}