using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace TestiranjeNetMaui;
 
    public class Kolo
    {
        public int id { get; set; }
        public string znamka { get; set; }
        public string slika { get; set; }
        public object lastnik { get; set; }
        public float trentnaLokacijaLongitude { get; set; }
        public float trentnaLokacijaLatitude { get; set; }
    }
[JsonSerializable(typeof(List<Kolo>))]
internal sealed partial class KolesaContext : JsonSerializerContext
{

}



