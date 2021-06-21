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
    [Authorize(Roles = "Manager")]

    public class ConsentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consents
        public async Task<IActionResult> Index()
        {
            var pfeContext = _context.Consents.Include(c => c.Brand).Include(c => c.Contact);
            return View(await pfeContext.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandNom");
                ViewData["ContactID"] = new SelectList(_context.Contacts, "ContactID", "LastName");
                return View(new Consent());
            }
            else
            {
                var consent = await _context.Consents.FindAsync(id);
                if (consent == null)
                {
                    return NotFound();
                }
                ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandNom", consent.BrandID);
                ViewData["ContactID"] = new SelectList(_context.Contacts, "ContactID", "LastName", consent.ContactID);
                return View(consent);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ConsentID,Title,ContactID,BrandID,C_CreatedOn,C_CreatedBy,Statut")] Consent consent)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    consent.C_CreatedBy = HttpContext.User.Identity.Name;
                    _context.Add(consent);
                    var change_Log = new Change_Log
                    {
                        Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "Consent", consent.ConsentID, consent.Title)
                    };

                    _context.Change_Log.Add(change_Log);
                    await _context.SaveChangesAsync();
                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(consent);
                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "modifiée", "Consent", consent.ConsentID, consent.Title)
                        };

                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ConsentExists(consent.ConsentID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.Consents.ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", consent) });
        }


        // GET: Consents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consent = await _context.Consents
                .Include(c => c.Brand)
                .Include(c => c.Contact)
                .FirstOrDefaultAsync(m => m.ConsentID == id);
            if (consent == null)
            {
                return NotFound();
            }

            return View(consent);
        }

        // POST: Consents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consent = await _context.Consents.FindAsync(id);
            _context.Consents.Remove(consent);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "supprimée", "Consent", consent.ConsentID, consent.Title)
            };

            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.Consents.ToList()) });
        }

        private bool ConsentExists(int id)
        {
            return _context.Consents.Any(e => e.ConsentID == id);
        }
    }
}
