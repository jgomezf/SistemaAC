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
    public class EstudiantesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private EstudianteModels estudiante;

        public EstudiantesController(ApplicationDbContext context)
        {
            _context = context;
            estudiante = new EstudianteModels(context);
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estudiante.ToListAsync());
        }

        public List<IdentityError> guardarEstudiante(List<Estudiante> response, int funcion)
        {
            return estudiante.guardarEstudiante(response, funcion);
        }

        public List<object[]> filtrarEstudiantes(int numPagina, string valor, string order)
        {
            return estudiante.filtrarEstudiantes(numPagina, valor, order);
        }

        public List<Estudiante> getEstudiante(int id)
        {
            return estudiante.getEstudiante(id);
        }

        public List<IdentityError> deleteEstudiante(int id)
        {
            return estudiante.deleteEstudiante(id);
        }
        public int MyProperty { get; set; }
        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .SingleOrDefaultAsync(m => m.ID == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,ID,Apellidos,Nombres,FechadNacimeinto,Documento,Email,Telefono,Direccion,Estado")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante.SingleOrDefaultAsync(m => m.ID == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,ID,Apellidos,Nombres,FechadNacimeinto,Documento,Email,Telefono,Direccion,Estado")] Estudiante estudiante)
        {
            if (id != estudiante.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .SingleOrDefaultAsync(m => m.ID == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiante.SingleOrDefaultAsync(m => m.ID == id);
            _context.Estudiante.Remove(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiante.Any(e => e.ID == id);
        }
    }
}
