using Microsoft.EntityFrameworkCore;

class BazaDB : DbContext
{
    public BazaDB(DbContextOptions<BazaDB> options)
        : base(options) { }

    public DbSet<Uporabnik> Uporabniki => Set<Uporabnik>();
}