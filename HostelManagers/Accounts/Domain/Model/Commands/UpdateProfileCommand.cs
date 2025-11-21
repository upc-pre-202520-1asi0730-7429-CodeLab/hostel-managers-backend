using HostelManagers.Accounts.Domain.Model.ValueObjects;

namespace HostelManagers.Accounts.Domain.Model.Commands;

public record UpdateProfileCommand(int Id, string Names, Role Roles, int UserId, int SuscriptionId);