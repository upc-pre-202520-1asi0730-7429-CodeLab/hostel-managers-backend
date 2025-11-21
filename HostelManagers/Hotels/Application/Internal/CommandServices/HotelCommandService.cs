using HostelManagers.Hotels.Domain.Model.Aggregates;
using HostelManagers.Hotels.Domain.Model.Commands;
using HostelManagers.Hotels.Domain.Repositories;
using HostelManagers.Hotels.Domain.Services;
using HostelManagers.Shared.Domain.Repositories;

namespace HostelManagers.Hotels.Application.Internal.CommandServices;

public class HotelCommandService (IHotelRepository hotelRepository, IUnitOfWork unitOfWork) : IHotelCommandService
{
    public async Task<Hotel?> Handle(CreateHotelCommand command)
    {
        var hotel = new Hotel(command.Name, command.Images, command.Address, command.Phone);
        await hotelRepository.AddAsync(hotel); 
        await unitOfWork.CompleteAsync();
        return hotel;
    }

    public async Task<Hotel?> Handle(UpdateHotelCommand command)
    {
        var hotel = await hotelRepository.FindByIdAsync(command.Id);
        if (hotel == null) return null;
        hotel.Update(command.Name, command.Images, command.Address, command.Phone);
        hotelRepository.Update(hotel);
        await unitOfWork.CompleteAsync();
        return hotel;
    }

    public async Task<bool> Handle(DeleteHotelCommand command)
    {
        var hotel = await hotelRepository.FindByIdAsync(command.Id);
        if (hotel == null) return false;
        hotelRepository.Remove(hotel);
        await unitOfWork.CompleteAsync();
        return true;
    }
}