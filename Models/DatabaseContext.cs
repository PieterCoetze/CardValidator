using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CardValidator.Models;

public partial class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> TCards { get; set; }

    public virtual DbSet<CardProvider> TCardProviders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=tcp:pieterdatabase.database.windows.net,1433;Initial Catalog=Pieter_Database;Persist Security Info=False;User ID=ptrcoetzee45@gmail.com@pieterdatabase;Password=pieter95Sandbox;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__tCards__55FECDAE7A2D4747");

            entity.ToTable("tCards");

            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CardProvider).WithMany(p => p.TCards)
                .HasForeignKey(d => d.CardProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tCards__CardProv__60A75C0F");
        });

        modelBuilder.Entity<CardProvider>(entity =>
        {
            entity.HasKey(e => e.CardProviderId).HasName("PK__tCardPro__749595128B6BB7E9");

            entity.ToTable("tCardProviders");

            entity.Property(e => e.CardProviderName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Configured).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
