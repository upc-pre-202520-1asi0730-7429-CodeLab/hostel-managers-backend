using HostelManagers.Rooms.Domain.Model.Aggregates;
using HostelManagers.Rooms.Domain.Model.Commands;
using HostelManagers.Rooms.Domain.Repositories;
using HostelManagers.Rooms.Domain.Services;
using HostelManagers.Shared.Domain.Repositories;

namespace HostelManagers.Rooms.Application.Internal.CommandServices;

public class RoomCommandService (IRoomRepository roomRepository, IUnitOfWork unitOfWork) : IRoomCommandService
{
    public async Task<Room?> Handle(CreateRoomCommand command)
    {
        var hotel = new Room(command.Type, command.Price);
        await roomRepository.AddAsync(hotel); 
        await unitOfWork.CompleteAsync();
        return hotel;
    }

    public async Task<Room?> Handle(UpdateRoomCommand command)
    {
        var hotel = await roomRepository.FindByIdAsync(command.Id);
        if (hotel == null) return null;
        hotel.Update(command.Type, command.Price);
        roomRepository.Update(hotel);
        await unitOfWork.CompleteAsync();
        return hotel;
    }

    public async Task<bool> Handle(DeleteRoomCommand command)
    {
        var hotel = await roomRepository.FindByIdAsync(command.Id);
        if (hotel == null) return false;
        roomRepository.Remove(hotel);
        await unitOfWork.CompleteAsync();
        return true;
    }
}