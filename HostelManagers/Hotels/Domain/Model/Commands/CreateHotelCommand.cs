namespace HostelManagers.Hotels.Domain.Model.Commands;

public record CreateHotelCommand(string Name, string Images, string Address, string Phone, int UserId);