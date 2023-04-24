using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;
using ManageContacts.Entity.Groups;
using ManageContacts.Entity.Roles;
using ManageContacts.Entity.Users;

namespace ManageContacts.Entity.Contacts;

public class Contact : IFullAuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ContactId { get; set; }
    
    public Guid GroupId { get; set; }
    
    #region [AUDIT PROPERTIES]
    public bool Deleted { get; set; }
    public DateTime CreatedTime { get; set; }
    public Guid? CreatorId { get; set; }
    public DateTime? ModifiedTime { get; set; }
    public Guid? ModifierId { get; set; }
    #endregion [AUDIT PROPERTIES]
    
    #region [REFERENCE PROPERTIES]
    [ForeignKey(nameof(CreatorId))]
    public virtual User Creator { get; set; }

    [ForeignKey(nameof(ModifierId))]
    public virtual User Modifier { get; set; }
    
    [ForeignKey(nameof(GroupId))]
    public virtual Group Group { get; set; }
    #endregion [REFERENCE PROPERTIES]
}