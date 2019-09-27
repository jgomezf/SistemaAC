using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaAC.Data;
using SistemaAC.ModelsClass;

namespace SistemaAC.Controllers
{

    public class ReportesController : Controller
    {
        private ApplicationDbContext _context;
        private CusoModels cursoModels;
        private MisCursosModels inscripcion;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
            cursoModels = new CusoModels(_context);
            inscripcion = new MisCursosModels(_context);

        }
        public IActionResult Index()
        {
            return View();
        }
        public List<object[]> reportesCursos(string valor, int numPagina, string order, int funcion)
        {
            String thead = "<tr>" + 
                    "<th>Nombre</th>" +
                    "<th>Descripción</th>" +
                    "<th>Créditos</th>" +
                    "<th>Horas</th>" +
                    "<th>Costo</th>" +
                    "<th>Estado</th>" +
                    "<th>Categoria</th>" +
                "</tr>";
            object[] datosObj = { thead };
            var reportes = cursoModels.filtrarCurso(numPagina, valor, order, funcion);
            reportes.Add(datosObj);
            return reportes;
        }
        public int[] estadosCursos()
        {
            return cursoModels.estadosCursos();
        }
    }
}