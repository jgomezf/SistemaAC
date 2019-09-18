using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAC.Data;
using SistemaAC.Models;
using SistemaAC.ModelsClass;

namespace SistemaAC.Controllers
{
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CusoModels cursoModels;
        public CursosController(ApplicationDbContext context)
        {
            _context = context;
            cursoModels = new CusoModels(context);
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public List<Categoria> getCategorias()
        {
            return cursoModels.getCategorias();
        }
        public List<IdentityError> agregarCurso(int id, string nombre, string descripcion, byte creditos, byte horas, decimal costo, Boolean estado, int categoria, string funcion)
        {
            return cursoModels.agregarCurso(id, nombre, descripcion, creditos, horas, costo, estado, categoria, funcion);
        }
        public List<object[]> filtrarCurso(int numPagina, string valor, string order)
        {
            return cursoModels.filtrarCurso(numPagina, valor, order);
        }
        public List<Curso> getCursos(int id)
        {
            return cursoModels.getCursos(id);
        }
        public List<IdentityError> editarCurso(int id, string nombre, string descripcion, byte creditos, byte horas, decimal costo, Boolean estado, int categoria, int funcion)
        {
            return cursoModels.editarCurso(id, nombre, descripcion, creditos, horas, costo, estado, categoria, funcion);
        }
    
    }
}
