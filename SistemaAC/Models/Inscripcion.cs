using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Inscripcion
    {
        public int InscripcionID { get; set; }
        public int Grado { get; set; }
        public int CursoID { get; set; }
        public int EstudienateID { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Pago { get; set; }
    }
}
