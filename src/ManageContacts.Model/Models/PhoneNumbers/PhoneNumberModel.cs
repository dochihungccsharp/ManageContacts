using ManageContacts.Model.Models.Contacts;
using ManageContacts.Model.Models.PhoneTypes;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.PhoneNumbers;

public class PhoneNumberModel
{
    public Guid Id { get; set; }
    public string Phone { get; set; }
    public string? Type { get; set; }
    public string? FormattedType { get; set; }
    public Guid PhoneTypeId { get; set; }
}