using HostelManagers.Reservations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace HostelManagers.Reservations.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    // Asegúrate de llamar a este método desde el método OnModelCreating de tu DbContext
    public static void ApplyReservationsConfiguration(this ModelBuilder builder)
    {
        // 1. Configurar la entidad Reservation
        builder.Entity<Reservation>().HasKey(r => r.Id);
        
        // 2. Clave primaria (Id)
        builder.Entity<Reservation>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        
        // 3. Propiedades Requeridas (no nulas)
        builder.Entity<Reservation>().Property(r => r.UserId).IsRequired();
        builder.Entity<Reservation>().Property(r => r.RoomId).IsRequired();
        builder.Entity<Reservation>().Property(r => r.CheckInDate).IsRequired();
        builder.Entity<Reservation>().Property(r => r.Status).IsRequired().HasMaxLength(50); // Opcional: limitar longitud
        
        // 4. Propiedad Anulable (CheckOutDate)
        // EF Core automáticamente infiere que DateTime? es NOT REQUIRED (puede ser nulo)
        // en la base de datos. Aun así, es bueno declararlo explícitamente si lo deseas:
        builder.Entity<Reservation>().Property(r => r.CheckOutDate).IsRequired();
        
        // 5. Índices (Opcional, pero recomendado para búsquedas rápidas)
        // Esto ayudará a las búsquedas por UserId y RoomId que definiste en tus Queries.
        builder.Entity<Reservation>().HasIndex(r => r.UserId);
        builder.Entity<Reservation>().HasIndex(r => r.RoomId);
    }
}