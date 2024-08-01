using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DelNaServerju;
using System.Xml;
namespace DelNaServerju
{
    public class PodatkiDb:DbContext
    {
        public PodatkiDb(DbContextOptions<PodatkiDb> options)
           : base(options)
        {
        }
        public virtual DbSet<Uporabnik> Uporabniki { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Uporabnik>()
                .HasIndex(u => u.Ime)
                .IsUnique();
         
            builder.Entity<Kolo>().Property(e => e.TrentnaLokacijaLatitude).HasPrecision(18, 5);
            builder.Entity<Kolo>().Property(e => e.TrentnaLokacijaLongitude).HasPrecision(18, 5);
        }
        public DbSet<DelNaServerju.Kolo> Kolo { get; set; } = default!;
    }
}
