#region

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Domain.Scaffolded;

[Table("Role")]
public class Role
{
    [Key] public int Id { get; set; }

    [StringLength(50)] [Unicode(false)] public string Name { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("Roles")]
    public virtual ICollection<User> Users { get; } = new List<User>();
}