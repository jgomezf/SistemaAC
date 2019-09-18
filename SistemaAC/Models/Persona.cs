using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Persona
    {
        public int ID { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public DateTime FechadNacimeinto { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public Boolean Estado { get; set; } = true;
    }
}
