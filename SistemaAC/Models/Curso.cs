using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Curso
    {
        public int CursoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte Creditos { get; set; }
        public byte Horas { get; set; }
        public decimal Costo { get; set; }
        public Boolean Estado { get; set; } = true;
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
    }
}
