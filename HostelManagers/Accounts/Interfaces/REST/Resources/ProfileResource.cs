using HostelManagers.Accounts.Domain.Model.ValueObjects;

namespace HostelManagers.Accounts.Interfaces.REST.Resources;

public record ProfileResource(int Id, string Names, Role Role, int UserId, int SuscriptionId);