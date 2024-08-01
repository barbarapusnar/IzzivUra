using Microsoft.EntityFrameworkCore;

public class PodatkiDb : DbContext
{
    public PodatkiDb(DbContextOptions<PodatkiDb> options)
        : base(options) { }

    public DbSet<Uporabnik> Uporabniki => Set<Uporabnik>();
    public DbSet<Tipi> Vrsta => Set<Tipi>();
    public DbSet<Registracije> Registracija => Set<Registracije>();
    protected override void OnModelCreating(ModelBuilder builder)
{
    builder.Entity<Uporabnik>()
        .HasIndex(u => u.Ime)
        .IsUnique();
}
}