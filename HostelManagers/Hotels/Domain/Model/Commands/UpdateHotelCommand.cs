namespace HostelManagers.Hotels.Domain.Model.Commands;

public record UpdateHotelCommand(int Id, string Name, string Images, string Address, string Phone, int ProfileId);