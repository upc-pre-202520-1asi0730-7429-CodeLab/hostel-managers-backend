using HostelManagers.Accounts.Domain.Model.Aggregates;
using HostelManagers.Shared.Domain.Repositories;

namespace HostelManagers.Accounts.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profiles>
{
}