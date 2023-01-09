using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Domain.Scaffolded;

[Table("UserInfo")]
public partial class UserInfo
{
    [StringLength(50)]
    [Unicode(false)]
    public string? Firstname { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Key]
    public int User { get; set; }

    [ForeignKey("User")]
    [InverseProperty("UserInfo")]
    public virtual User UserNavigation { get; set; } = null!;
}
