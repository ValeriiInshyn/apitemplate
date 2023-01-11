using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Server.Domain.Scaffolded;

namespace Server.Infrastructure.Database;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Group__3214EC0720C1BDF1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07F0A6FBA2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07FFA4C07D");

            entity.HasOne(d => d.Group).WithMany(p => p.Users).HasConstraintName("FK__User__GroupId__07C12930");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserToRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserToRol__RoleI__0B91BA14"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserToRol__UserI__0A9D95DB"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserToRo__AF2760AD32213FEE");
                        j.ToTable("UserToRole");
                    });
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.User).HasName("PK__UserInfo__BD20C6F0B66A97FC");

            entity.Property(e => e.User).ValueGeneratedNever();

            entity.HasOne(d => d.UserNavigation).WithOne(p => p.UserInfo).HasConstraintName("FK__UserInfo__User__0E6E26BF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
