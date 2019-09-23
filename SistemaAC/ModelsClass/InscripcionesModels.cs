using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAC.Data;
using SistemaAC.Models;

namespace SistemaAC.ModelsClass
{
    public class InscripcionesModels
    {
        private ApplicationDbContext context;

        public InscripcionesModels(ApplicationDbContext context)
        {
            this.context = context;
        }

        public String filtrarEstudiantes(string valor)
        {
            string dataFilter = "";
            if (valor != "null")
            {
                var estudiante = context.Estudiante.OrderBy(p => p.Nombres).ToList();
                var query = estudiante.Where(p => p.Documento.StartsWith(valor) || p.Nombres.ToLower().StartsWith(valor.ToLower()) || p.Apellidos.ToLower().StartsWith(valor.ToLower()));
                foreach (var item in query)
                {
                    dataFilter += "<tr>" +
                       "<td>" + "<input type='checkbox'name='cboxEstudiante[]' id='cboxEstudiante' value='" + item.ID + "'>" + "</td>" +
                       "<td>" + item.Apellidos + " " + item.Nombres + "</td>" +
                       "<td>" + item.Documento + "</td>" +
                        "<td>" + item.Email + "</td>" +
                       "<td>" + item.Telefono + "</td>" +
                   "</tr>";
                }
            }
            return dataFilter;
        }

        internal List<Estudiante> getEstudiante(int id)
        {
            return context.Estudiante.Where(c => c.ID == id).ToList();
        }
    }
}
