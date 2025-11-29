using System.Text.Json.Serialization;
using HostelManagers.Hotels.Domain.Model.Aggregates;

namespace HostelManagers.IAM.Domain.Model.Aggregates;

/**
 * <summary>
 *     The user aggregate
 * </summary>
 * <remarks>
 *     This class is used to represent a user
 * </remarks>
 */
public class User(string username, string passwordHash)
{
    public User() : this(string.Empty, string.Empty)
    {
    }

    public int Id { get; }
    public string Username { get; private set; } = username;

    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
    
    public string Names { get; set; } = string.Empty;
    
    public string Roles { get; set; } = string.Empty;
    
    public ICollection<Hotel> Hotels { get; private set; } = new List<Hotel>();

    /**
     * <summary>
     *     Update the username
     * </summary>
     * <param name="username">The new username</param>
     * <returns>The updated user</returns>
     */
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    /**
     * <summary>
     *     Update the password hash
     * </summary>
     * <param name="passwordHash">The new password hash</param>
     * <returns>The updated user</returns>
     */
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
    
    public User Update (string names, string roles)
    {
        this.Names = names;
        this.Roles = roles;
        return this;
    }
}