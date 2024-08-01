using Microsoft.AspNetCore.SignalR.Protocol;

namespace DelNaServerju
{
    public class Uporabnik
    {
        public int Id { get; set; }
        public string  Ime { get; set; }
        public string KodiranoGeslo { get; set; }
        public bool JeAktiven { get; set; }
    }
}
