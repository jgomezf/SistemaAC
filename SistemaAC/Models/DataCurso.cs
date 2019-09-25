using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class DataCurso : Inscripcion
    {
        public string Curso { get; set; }
        public string Estudiante { get; set; }
        public string Docente { get; set; }
    }
}
