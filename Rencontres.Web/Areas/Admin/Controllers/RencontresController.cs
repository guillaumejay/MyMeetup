using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rencontres.Metier.Infrastructure;
using Rencontres.Metier.Modeles;

namespace Rencontres.Web.Areas.Admin
{
    [Area("Admin")]
    public class RencontresController : Controller
    {
        private readonly RencontresContext _context;

        public RencontresController(RencontresContext context)
        {
            _context = context;
        }

        // GET: Admin/Rencontres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rencontres.ToListAsync());
        }

        // GET: Admin/Rencontres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rencontre = await _context.Rencontres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rencontre == null)
            {
                return NotFound();
            }

            return View(rencontre);
        }

        // GET: Admin/Rencontres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Rencontres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titre,DateDebut,DateFin,EstVisible,DateOuvertureInscription,MontantVerse,Id,CreationLe,ModifieLe")] Rencontre rencontre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rencontre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rencontre);
        }

        // GET: Admin/Rencontres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rencontre = await _context.Rencontres.FindAsync(id);
            if (rencontre == null)
            {
                return NotFound();
            }
            return View(rencontre);
        }

        // POST: Admin/Rencontres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Titre,DateDebut,DateFin,EstVisible,DateOuvertureInscription,MontantVerse,Id,CreationLe,ModifieLe")] Rencontre rencontre)
        {
            if (id != rencontre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rencontre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RencontreExists(rencontre.Id))
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
            return View(rencontre);
        }

        // GET: Admin/Rencontres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rencontre = await _context.Rencontres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rencontre == null)
            {
                return NotFound();
            }

            return View(rencontre);
        }

        // POST: Admin/Rencontres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rencontre = await _context.Rencontres.FindAsync(id);
            _context.Rencontres.Remove(rencontre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RencontreExists(int id)
        {
            return _context.Rencontres.Any(e => e.Id == id);
        }
    }
}
