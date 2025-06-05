using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using WebApplication1.Persistence.Models;

namespace WebApplication1.Persistence;

public partial class UninventoryDBContext : DbContext
{
    public UninventoryDBContext()
    {
    }

    public UninventoryDBContext(DbContextOptions<UninventoryDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=uninventory_equipment;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PRIMARY");

            entity.ToTable("equipment");

            entity.HasIndex(e => e.CategoryId, "FK_equipment_categories");

            entity.Property(e => e.EquipmentId)
                .HasColumnType("int(11)")
                .HasColumnName("equipmentId");
            entity.Property(e => e.CategoryId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .HasColumnName("image");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Model)
                .HasMaxLength(30)
                .HasColumnName("model");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .HasColumnName("serialNumber");
            entity.Property(e => e.Specifications)
                .HasMaxLength(100)
                .HasColumnName("specifications");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.WarrantyDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_equipment_categories");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
