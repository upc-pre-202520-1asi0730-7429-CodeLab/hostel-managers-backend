using System.Text.Json.Serialization;
using HostelManagers.Accounts.Domain.Model.Aggregates;

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
    
    public ICollection<Profiles> Profiles { get; set; } = new List<Profiles>();

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
}