
    public class Registracije
    {
        public int Id { get; set; }
        public int IdUporabnika { get; set; }
        public int IdTipa { get; set; }
        public DateOnly Datum { get; set; }
        public TimeOnly Čas { get; set; }
        public virtual Uporabnik Uporabnik { get; set; }
        public virtual Tipi Tipi { get; set; }
    }

