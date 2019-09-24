using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaAC.Data;
using SistemaAC.Models;
using SistemaAC.ModelsClass;

namespace SistemaAC.Controllers
{
    public class InscripcionesController : Controller
    {
        private InscripcionesModels inscripcion;

        public InscripcionesController(ApplicationDbContext context)
        {
            inscripcion = new InscripcionesModels(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public String filtrarEstudiantesIns(string valor)
        {
            return inscripcion.filtrarEstudiantes(valor);
        }

        public List<Estudiante> getEstudiante(int id)
        {
            return inscripcion.getEstudiante(id);
        }

        public String filtrarCurso(string valor)
        {
            return inscripcion.filtrarCurso(valor);
        }

        public List<Curso> getCurso(int id)
        {
            return inscripcion.getCursos(id);
        }

        public List<IdentityError> guardarCursos(List<Inscripcion> listCursos)
        {
            return inscripcion.guardarCursos(listCursos);
        }

        public String getCategorias(int id)
        {
            return inscripcion.getCategorias(id);
        }
    }
}