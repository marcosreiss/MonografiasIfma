using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonografiasIfma.Data;
using MonografiasIfma.Models;
using System.Security.Cryptography;

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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome");
            ViewData["OrientadorId"] = new SelectList(_context.Orientadores, "Id", "Nome");
            return View();
        }

        // POST: Monografia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,DataApresentacao,QtPaginas,AlunoId,OrientadorId")] Monografia monografia, IFormFile MonografiaPDF)
        {

            if (MonografiaPDF != null && MonografiaPDF.Length > 0)
            {
                if (Path.GetExtension(MonografiaPDF.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        MonografiaPDF.CopyTo(ms);
                        monografia.Pdf_ArquivoBinario = ms.ToArray();

                        // Calcula o checksum SHA-256 do conte�do do arquivo PDF
                        string checksum = CalculateChecksum(ms);
                        monografia.checksum = checksum;

                        if (_context.Monografias.Any(m => m.checksum == checksum))
                        {
                            ModelState.AddModelError("Pdf_ArquivoBinario", "*J� existe uma monografia com o mesmo arquivo PDF*");
                            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome");
                            ViewData["OrientadorId"] = new SelectList(_context.Orientadores, "Id", "Nome");
                            return View(monografia);
                        }

                    }
                }
                else
                {
                    // Define uma mensagem de erro se o arquivo n�o for um PDF
                    ModelState.AddModelError("Pdf_ArquivoBinario", "*O arquivo tem que ser em formato de PDF*");
                    ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome");
                    ViewData["OrientadorId"] = new SelectList(_context.Orientadores, "Id", "Nome");
                    return View(monografia);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(monografia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Restante do c�digo para lidar com outros erros de valida��o
                ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome");
                ViewData["OrientadorId"] = new SelectList(_context.Orientadores, "Id", "Nome");
                return View(monografia);
            }
        }

        private string CalculateChecksum(MemoryStream stream)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(stream.ToArray());
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
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
