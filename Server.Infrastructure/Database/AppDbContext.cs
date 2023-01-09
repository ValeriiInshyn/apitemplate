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
            entity.HasKey(e => e.Id).HasName("PK__Group__3214EC07A0151BD8");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07742E0F91");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0746447C3D");

            entity.HasOne(d => d.Group).WithMany(p => p.Users).HasConstraintName("FK__User__GroupId__6C190EBB");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users).HasConstraintName("FK__User__Role__6D0D32F4");

            entity.HasMany(d => d.Roles).WithMany(p => p.UsersNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "UserToRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserToRol__RoleI__7B5B524B"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserToRol__UserI__7A672E12"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserToRo__AF2760AD33F8B625");
                        j.ToTable("UserToRole");
                    });
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.User).HasName("PK__UserInfo__BD20C6F0B1780F61");

            entity.Property(e => e.User).ValueGeneratedNever();

            entity.HasOne(d => d.UserNavigation).WithOne(p => p.UserInfo).HasConstraintName("FK__UserInfo__User__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
