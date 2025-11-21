namespace HostelManagers.Accounts.Domain.Model.ValueObjects;

/// <summary>
///     Representa los roles válidos para usuarios del sistema.
///     El valor numérico asociado de cada rol es:
///     - Admin: 0
///     - Client: 1
///     Estos valores numéricos pueden ser usados para almacenar el rol en base de datos.
/// </summary>
public enum Role
{
    Admin, // -> 0
    Client // -> 1
}