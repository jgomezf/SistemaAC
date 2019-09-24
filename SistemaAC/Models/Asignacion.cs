using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Asignacion
    {
        public int AsignacionID { get; set; }
        public int CursoID { get; set; }
        public int InstructorID { get; set; }
        public DateTime Fecha { get; set; }
    }
}
