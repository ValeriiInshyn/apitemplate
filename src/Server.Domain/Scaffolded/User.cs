#region

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Domain.Scaffolded;

[Table("User")]
public class User
{
    [Key] public int Id { get; set; }

    [StringLength(50)] [Unicode(false)] public string UserName { get; set; } = null!;

    [StringLength(50)] [Unicode(false)] public string Password { get; set; } = null!;

    [StringLength(50)] [Unicode(false)] public string Email { get; set; } = null!;

    public int? GroupId { get; set; }

    public bool? IsDeleted { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("Users")]
    public virtual Group? Group { get; set; }

    [InverseProperty("UserNavigation")] public virtual UserInfo? UserInfo { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Role> Roles { get; } = new List<Role>();
}