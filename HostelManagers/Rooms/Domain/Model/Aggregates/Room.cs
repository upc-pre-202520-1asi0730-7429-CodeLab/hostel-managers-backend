using HostelManagers.Hotels.Domain.Model.Aggregates;

namespace HostelManagers.Rooms.Domain.Model.Aggregates;

public partial class Room
{
    public int Id { get; set; }
    
    public string Imagen { get; set; }
    public string Type { get; private set; }
    public decimal Price { get; private set; }
    
    public int HotelId { get; set; }              
    public Hotel Hotel { get; set; }              
  
    protected Room()
    {
        Imagen = string.Empty;
        Type = string.Empty;
        Price = 0;
    }
    
    public Room(string imagen, string type, decimal price, int hotelId)
    {
        Imagen = imagen;
        Type = type;
        Price = price;
        HotelId = hotelId;
    }
    
    public Room Update(string imagen, string type, decimal price, int hotelId)
    {
        Imagen = imagen;
        Type = type;
        Price = price;
        HotelId = hotelId;
        return this;
    }
}