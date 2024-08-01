using System;
using System.Collections.Generic;

namespace OUporabnikih;

public partial class Uporabniki
{
    public int Id { get; set; }

    public string HashiranoGeslo { get; set; } = null!;

    public string Ime { get; set; } = null!;

    public int JeAktiven { get; set; }

    public virtual ICollection<Registracija> Registracijas { get; set; } = new List<Registracija>();
}
