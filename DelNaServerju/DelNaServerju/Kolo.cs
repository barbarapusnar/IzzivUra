using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace DelNaServerju
{
    public class Kolo
    {
        public int Id { get; set; }
        public string Znamka { get; set; }
        public string  Slika { get; set; }
        public virtual Uporabnik Lastnik { get; set; }//za FK
        public decimal TrentnaLokacijaLongitude { get; set; }
        public decimal TrentnaLokacijaLatitude { get; set; }
    }
}
