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
    public class MonografiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonografiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Monografia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Monografias.Include(m => m.Aluno).Include(m => m.Orientador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Monografia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Monografias == null)
            {
                return NotFound();
            }

            var monografia = await _context.Monografias
                .Include(m => m.Aluno)
                .Include(m => m.Orientador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monografia == null)
            {
                return NotFound();
            }

            return View(monografia);
        }

        // GET: Monografia/Create
        public IActionResult Create()
        {
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Campus");
            ViewData["OrientadorId"] = new SelectList(_context.Orientadores, "Id", "Campus");
            return View();
        }

        // POST: Monografia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,DataApresentacao,QtPaginas,Pdf_ArquivoBinario,AlunoId,OrientadorId")] Monografia monografia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monografia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Campus", monografia.AlunoId);
            ViewData["OrientadorId"] = new SelectList(_context.Orientadores, "Id", "Campus", monografia.OrientadorId);
            return View(monografia);
        }

        // GET: Monografia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Monografias == null)
            {
                return NotFound();
            }

            var monografia = await _context.Monografias.FindAsync(id);
            if (monografia == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Campus", monografia.AlunoId);
            ViewData["OrientadorId"] = new SelectList(_context.Orientadores, "Id", "Campus", monografia.OrientadorId);
            return View(monografia);
        }

        // POST: Monografia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,DataApresentacao,QtPaginas,Pdf_ArquivoBinario,AlunoId,OrientadorId")] Monografia monografia)
        {
            if (id != monografia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monografia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonografiaExists(monografia.Id))
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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Campus", monografia.AlunoId);
            ViewData["OrientadorId"] = new SelectList(_context.Orientadores, "Id", "Campus", monografia.OrientadorId);
            return View(monografia);
        }

        // GET: Monografia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Monografias == null)
            {
                return NotFound();
            }

            var monografia = await _context.Monografias
                .Include(m => m.Aluno)
                .Include(m => m.Orientador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monografia == null)
            {
                return NotFound();
            }

            return View(monografia);
        }

        // POST: Monografia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Monografias == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Monografias'  is null.");
            }
            var monografia = await _context.Monografias.FindAsync(id);
            if (monografia != null)
            {
                _context.Monografias.Remove(monografia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonografiaExists(int id)
        {
            return (_context.Monografias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
