using HostelManagers.Accounts.Domain.Model.Aggregates;
using HostelManagers.Rooms.Domain.Model.Aggregates;

namespace HostelManagers.Hotels.Domain.Model.Aggregates;

public class Hotel
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Images { get; private set; }
    public string Address { get; private set; }
    public string Phone { get; private set; }
    
    public Profiles Profile { get; set; } 
    public int ProfileId { get; set; }
    
    public ICollection<Room> Rooms { get; private set; } = new List<Room>();
    
    
    protected Hotel()
    {
        Name = string.Empty;
        Images = string.Empty;
        Address = string.Empty;
        Phone = string.Empty;
    }
    
    public Hotel(string name, string images, string address, string phone, int profileId)
    {
        Name = name;
        Images = images;
        Address = address;
        Phone = phone;
        ProfileId = profileId;
    }
    
    public Hotel Update(string name, string images, string address, string phone, int profileId)
    {
        Name = name;
        Images = images;
        Address = address;
        Phone = phone;
        ProfileId = profileId;
        return this;
    }
}