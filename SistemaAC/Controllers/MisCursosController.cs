using Microsoft.AspNetCore.Mvc;
using SistemaAC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Controllers
{
    public class MisCursosController : Controller
    {
        public MisCursosController(ApplicationDbContext context)
        {
           
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
