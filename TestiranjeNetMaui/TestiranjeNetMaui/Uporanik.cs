using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestiranjeNetMaui
{
    public class Uporanik
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string HashiranoGeslo { get; set; }
        public bool JeAktiven { get; set; }
    }
}
