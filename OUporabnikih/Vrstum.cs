using System;
using System.Collections.Generic;

namespace OUporabnikih;

public partial class Vrstum
{
    public int Id { get; set; }

    public string Opis { get; set; } = null!;

    public virtual ICollection<Registracija> Registracijas { get; set; } = new List<Registracija>();
}
