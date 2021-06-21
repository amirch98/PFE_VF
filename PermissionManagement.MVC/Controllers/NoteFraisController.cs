using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Data;
using PermissionManagement.MVC.Models;
using static PermissionManagement.MVC.Helper;


namespace PermissionManagement.MVC.Controllers
{
    [Authorize(Roles = "Responsable_Comercial,Manager")]

    public class NoteFraisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoteFraisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NoteFrais
        public async Task<IActionResult> Index()
        {
            return View(await _context.NoteFrais.ToListAsync());
        }


        // GET: NoteFrais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteFrais = await _context.NoteFrais
                .FirstOrDefaultAsync(m => m.NoteFraisID == id);
            if (noteFrais == null)
            {
                return NotFound();
            }

            return View(noteFrais);
        }

        // POST: NoteFrais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noteFrais = await _context.NoteFrais.FindAsync(id);
            _context.NoteFrais.Remove(noteFrais);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "supprimée", "Contact", noteFrais.NoteFraisID, "#")
            };
            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteFraisExists(int id)
        {
            return _context.NoteFrais.Any(e => e.NoteFraisID == id);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int id)
        {
            var notedefrais = await _context.NoteFrais.FindAsync(id);

            notedefrais.statut = "Accepté";
            _context.NoteFrais.Update(notedefrais);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, notedefrais.statut, "Note de Frais", notedefrais.NoteFraisID, notedefrais.type)
            };
            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();

            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.NoteFrais.ToList()) });
        }

        [HttpPost]
        public async Task<IActionResult> Refuse(int id)
        {
            var notedefrais = await _context.NoteFrais.FindAsync(id);

            notedefrais.statut = "Refusé";
            _context.NoteFrais.Update(notedefrais);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, notedefrais.statut, "Note de Frais", notedefrais.NoteFraisID, notedefrais.type)
            };
            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();

            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.NoteFrais.ToList()) });
        }
    }
}
