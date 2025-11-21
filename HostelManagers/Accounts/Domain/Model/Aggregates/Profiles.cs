using HostelManagers.Accounts.Domain.Model.ValueObjects;
using HostelManagers.Hotels.Domain.Model.Aggregates;
using HostelManagers.IAM.Domain.Model.Aggregates;
using HostelManagers.Suscriptions.Domain.Model.Aggregates;

namespace HostelManagers.Accounts.Domain.Model.Aggregates;

public partial class Profiles
{
    public int Id { get; private set; }
    public string Names { get; private set; }
    public Role Roles { get; private set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int SuscriptionId { get; set; }
    public Suscription Suscription { get; set; }
    public ICollection<Hotel> Hotels { get; private set; } = new List<Hotel>();
    
    protected Profiles()
    {
        Names = string.Empty;
        Roles = Role.Admin;
    }

    public Profiles(string names, Role roles, int userId, int suscriptionId)
    {
        Names = names;
        Roles = roles;
        UserId = userId;
        SuscriptionId = suscriptionId;
    }

    public Profiles Update(string names, Role roles, int userId, int suscriptionId)
    {
        Names = names;
        Roles = roles;
        UserId = userId;
        SuscriptionId = suscriptionId;
        return this;
    }
}