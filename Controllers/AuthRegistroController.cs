using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practicas_ASP.NET.Methods;
using Practicas_ASP.NET.Models;

namespace Practicas_ASP.NET.Controllers
{
    public class AuthRegistroController : Controller
    {
        private Encriptar encript = new Encriptar();
        private readonly RegistroContext _context;

        public AuthRegistroController(RegistroContext context)
        {
            _context = context;
        }

        // GET: AuthRegistro
        public async Task<IActionResult> Index()
        {
            return View(await _context.AuthRegistros.ToListAsync());
        }

        // GET: AuthRegistro/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authRegistro = await _context.AuthRegistros
                .FirstOrDefaultAsync(m => m.JwtId == id);
            if (authRegistro == null)
            {
                return NotFound();
            }

            return View(authRegistro);
        }

        // GET: AuthRegistro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthRegistro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JwtId,Username,Mail,Password,AuditFechaGeneracion")] AuthRegistro authRegistro)
        {
            if (ModelState.IsValid)
            {
                authRegistro.JwtId = Guid.NewGuid();

                //Encripto la contrasena en SQL
                authRegistro.Password = encript.EncryptarPassword(authRegistro.Password);

                _context.Add(authRegistro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authRegistro);
        }

        // GET: AuthRegistro/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authRegistro = await _context.AuthRegistros.FindAsync(id);
            if (authRegistro == null)
            {
                return NotFound();
            }
            return View(authRegistro);
        }

        // POST: AuthRegistro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("JwtId,Username,Mail,Password,AuditFechaGeneracion")] AuthRegistro authRegistro)
        {
            if (id != authRegistro.JwtId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authRegistro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthRegistroExists(authRegistro.JwtId))
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
            return View(authRegistro);
        }

        // GET: AuthRegistro/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authRegistro = await _context.AuthRegistros
                .FirstOrDefaultAsync(m => m.JwtId == id);
            if (authRegistro == null)
            {
                return NotFound();
            }

            return View(authRegistro);
        }

        // POST: AuthRegistro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var authRegistro = await _context.AuthRegistros.FindAsync(id);
            if (authRegistro != null)
            {
                _context.AuthRegistros.Remove(authRegistro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthRegistroExists(Guid id)
        {
            return _context.AuthRegistros.Any(e => e.JwtId == id);
        }
    }
}
