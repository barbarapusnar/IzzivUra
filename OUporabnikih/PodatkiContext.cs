using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OUporabnikih;

public partial class PodatkiContext : DbContext
{
    public PodatkiContext()
    {
    }

    public PodatkiContext(DbContextOptions<PodatkiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Registracija> Registracijas { get; set; }

    public virtual DbSet<Uporabniki> Uporabnikis { get; set; }

    public virtual DbSet<Vrstum> Vrsta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("DataSource=D:\\\\Challenger\\\\IzzivUra\\\\OUporabnikih\\\\Podatki.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registracija>(entity =>
        {
            entity.ToTable("Registracija");

            entity.HasIndex(e => e.TipiId, "IX_Registracija_TipiId");

            entity.HasIndex(e => e.UporabnikId, "IX_Registracija_UporabnikId");

            entity.HasOne(d => d.Tipi).WithMany(p => p.Registracijas).HasForeignKey(d => d.TipiId);

            entity.HasOne(d => d.Uporabnik).WithMany(p => p.Registracijas).HasForeignKey(d => d.UporabnikId);
        });

        modelBuilder.Entity<Uporabniki>(entity =>
        {
            entity.ToTable("Uporabniki");

            entity.HasIndex(e => e.Ime, "IX_Uporabniki_Ime").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
