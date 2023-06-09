using ManageContacts.Model.Models.Contacts;
using ManageContacts.Model.Models.Users;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Groups;

public class GroupModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Deleted { get; set; }
    public DateTime CreatedTime { get; set; }
}