using HostelManagers.Accounts.Domain.Model.Aggregates;
using HostelManagers.Accounts.Domain.Model.Commands;
using HostelManagers.Accounts.Domain.Repositories;
using HostelManagers.Accounts.Domain.Services;
using HostelManagers.Shared.Domain.Repositories;

namespace HostelManagers.Accounts.Application.Internal.CommandServices;

public class ProfileCommandService(IProfileRepository profileRepository, IUnitOfWork unitOfWork)
    : IProfileCommandService
{
    public async Task<Profiles?> Handle(CreateProfileCommand command)
    {
        var profile = new Profiles(command.Names, command.Roles, command.UserId, command.SuscriptionId);
        await profileRepository.AddAsync(profile); 
        await unitOfWork.CompleteAsync();
        return profile;
    }

    public async Task<Profiles?> Handle(UpdateProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.Id);
        if (profile == null) return null;
        profile.Update(command.Names, command.Roles, command.UserId, command.SuscriptionId);
        profileRepository.Update(profile);
        await unitOfWork.CompleteAsync();
        return profile;
    }

    public async Task<bool> Handle(DeleteProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.Id);
        if (profile == null) return false;
        profileRepository.Remove(profile);
        await unitOfWork.CompleteAsync();
        return true;
    }
}