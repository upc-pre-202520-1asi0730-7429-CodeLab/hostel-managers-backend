using HostelManagers.Hotels.Domain.Model.Aggregates;
using HostelManagers.Rooms.Domain.Model.ValueObjects;

namespace HostelManagers.Rooms.Domain.Model.Aggregates;

public partial class Room
{
    public int Id { get; set; }
    public Typeroom Type { get; private set; }
    public decimal Price { get; private set; }
    
    public int HotelId { get; set; }              
    public Hotel Hotel { get; set; }              
    
    protected Room()
    {
        Type = Typeroom.Individual;
        Price = 0;
    }
    
    public Room(Typeroom type, decimal price)
    {
        Type = type;
        Price = price;
    }
    
    public Room Update(Typeroom type, decimal price)
    {
        Type = type;
        Price = price;
        return this;
    }
}