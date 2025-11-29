using HostelManagers.IAM.Domain.Model.Aggregates;
using HostelManagers.Rooms.Domain.Model.Aggregates;

namespace HostelManagers.Hotels.Domain.Model.Aggregates;

public class Hotel
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Images { get; private set; }
    public string Address { get; private set; }
    public string Phone { get; private set; }
    
    public ICollection<Room> Rooms { get; private set; } = new List<Room>();
    
    public int UserId { get; set; }              
    public User User { get; set; }
    
    
    protected Hotel()
    {
        Name = string.Empty;
        Images = string.Empty;
        Address = string.Empty;
        Phone = string.Empty;
        UserId = 0;
    }
    
    public Hotel(string name, string images, string address, string phone, int userId)
    {
        Name = name;
        Images = images;
        Address = address;
        Phone = phone;
        UserId = userId;
    }
    
    public Hotel Update(string name, string images, string address, string phone, int userId)
    {
        Name = name;
        Images = images;
        Address = address;
        Phone = phone;
        UserId = userId;
        return this;
    }
}