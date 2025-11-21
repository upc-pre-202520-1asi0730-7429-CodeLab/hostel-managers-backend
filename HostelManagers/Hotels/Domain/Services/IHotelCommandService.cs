using HostelManagers.Hotels.Domain.Model.Aggregates;
using HostelManagers.Hotels.Domain.Model.Commands;

namespace HostelManagers.Hotels.Domain.Services;

public interface IHotelCommandService
{
    Task<Hotel?> Handle(CreateHotelCommand command);
    Task<Hotel?> Handle(UpdateHotelCommand command);
    Task<bool> Handle(DeleteHotelCommand command);
}