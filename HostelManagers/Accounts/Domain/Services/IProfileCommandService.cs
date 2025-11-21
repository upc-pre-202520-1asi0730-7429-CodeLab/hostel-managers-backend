using HostelManagers.Accounts.Domain.Model.Aggregates;
using HostelManagers.Accounts.Domain.Model.Commands;

namespace HostelManagers.Accounts.Domain.Services;

public interface IProfileCommandService
{
    Task<Profiles?> Handle(CreateProfileCommand command);
    Task<Profiles?> Handle(UpdateProfileCommand command);
    Task<bool> Handle(DeleteProfileCommand command);
}