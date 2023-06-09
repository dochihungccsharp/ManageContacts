using ManageContacts.Model.Models.Contacts;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Companies;

public class CompanyModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}