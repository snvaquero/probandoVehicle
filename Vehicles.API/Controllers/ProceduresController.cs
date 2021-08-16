using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProceduresController : Controller
    {
        private readonly DataContext _context;

        public ProceduresController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Procedures.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procedure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(procedure);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Procedure procedure = await _context.Procedures.FindAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }
            return View(procedure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Procedure procedure)
        {
            if (id != procedure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedureExists(procedure.Id))
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
            return View(procedure);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Procedure procedure = await _context.Procedures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedure == null)
            {
                return NotFound();
            }

            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedureExists(int id)
        {
            return _context.Procedures.Any(e => e.Id == id);
        }
    }
}
