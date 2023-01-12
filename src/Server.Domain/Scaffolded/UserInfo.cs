#region

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Domain.Scaffolded;

[Table("UserInfo")]
public class UserInfo
{
    [StringLength(50)] [Unicode(false)] public string? Firstname { get; set; }

    [StringLength(50)] [Unicode(false)] public string? LastName { get; set; }

    [Key] public int User { get; set; }

    [ForeignKey("User")]
    [InverseProperty("UserInfo")]
    public virtual User UserNavigation { get; set; } = null!;
}