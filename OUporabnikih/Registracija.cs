using System;
using System.Collections.Generic;

namespace OUporabnikih;

public partial class Registracija
{
    public int Id { get; set; }

    public string Datum { get; set; } = null!;

    public int IdTipa { get; set; }

    public int IdUporabnika { get; set; }

    public int TipiId { get; set; }

    public int UporabnikId { get; set; }

    public string Čas { get; set; } = null!;

    public virtual Vrstum Tipi { get; set; } = null!;

    public virtual Uporabniki Uporabnik { get; set; } = null!;
}
