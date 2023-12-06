using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonografiasIfma.Data;
using MonografiasIfma.Models;

namespace MonografiasIfma.Controllers
{
    // [Authorize] acessível apenas a usuários autenticados (funcionários do setor), habilitar quando a autenticação estiver funcionando
    public class OrientadorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrientadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orientador
        public async Task<IActionResult> Index()
        {
            return _context.Orientadores != null ?
                        View(await _context.Orientadores.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Orientadores'  is null.");
        }

        // GET: Orientador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orientadores == null)
            {
                return NotFound();
            }

            var orientador = await _context.Orientadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orientador == null)
            {
                return NotFound();
            }

            return View(orientador);
        }

        // GET: Orientador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orientador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Siap,Id,Nome,Email,Telefone,Cidade,Campus,UserType")] Orientador orientador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orientador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orientador);
        }

        // GET: Orientador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orientadores == null)
            {
                return NotFound();
            }

            var orientador = await _context.Orientadores.FindAsync(id);
            if (orientador == null)
            {
                return NotFound();
            }
            return View(orientador);
        }

        // POST: Orientador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Siap,Id,Nome,Email,Telefone,Cidade,Campus,UserType")] Orientador orientador)
        {
            if (id != orientador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orientador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrientadorExists(orientador.Id))
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
            return View(orientador);
        }

        // GET: Orientador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orientadores == null)
            {
                return NotFound();
            }

            var orientador = await _context.Orientadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orientador == null)
            {
                return NotFound();
            }

            return View(orientador);
        }

        // POST: Orientador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orientadores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orientadores'  is null.");
            }
            var orientador = await _context.Orientadores.FindAsync(id);
            if (orientador != null)
            {
                _context.Orientadores.Remove(orientador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrientadorExists(int id)
        {
            return (_context.Orientadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
