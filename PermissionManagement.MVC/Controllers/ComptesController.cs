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
    public class ComptesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComptesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comptes
        public async Task<IActionResult> Index()
        {
            var pfeContext = _context.Comptes.Include(c => c.Segment);
            return View(await pfeContext.ToListAsync());
        }

        // GET: Comptes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compte = await _context.Comptes
                .Include(c => c.Segment)
                .FirstOrDefaultAsync(m => m.CompteID == id);
            if (compte == null)
            {
                return NotFound();
            }

            return View(compte);
        }
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {

                ViewData["SegmentID"] = new SelectList(_context.Segments.Where(s => s.SType == (SType)1).ToList(), "SegmentID", "SegmentID");
                return View(new Compte());

            }
            else
            {

                var compte = await _context.Comptes.FindAsync(id);
                if (compte == null)
                {
                    return NotFound();
                }
                ViewData["SegmentID"] = new SelectList(_context.Segments, "SegmentID", "SName", compte.SegmentID);
                return View(compte);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("CompteID,AccountName,Phone,Email,Adress,CType,SegmentID")] Compte compte)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _context.Add(compte);
                    var change_Log = new Change_Log
                    {
                        Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "Compte", compte.CompteID, compte.AccountName)
                    };
                    _context.Change_Log.Add(change_Log);
                    await _context.SaveChangesAsync();

                }



                //Update
                else
                {
                    try
                    {
                        _context.Update(compte);
                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "modifiée", "Compte", compte.CompteID, compte.AccountName)
                        };
                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CompteExists(compte.CompteID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.Comptes.Include(c => c.Segment).ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", compte) });
        }


        

        // GET: Comptes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compte = await _context.Comptes
                .Include(c => c.Segment)
                .FirstOrDefaultAsync(m => m.CompteID == id);
            if (compte == null)
            {
                return NotFound();
            }

            return View(compte);
        }

        // POST: Comptes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compte = await _context.Comptes.FindAsync(id);
            _context.Comptes.Remove(compte);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "supprimée", "Compte", compte.CompteID, compte.AccountName)
            };
            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.Comptes.Include(c => c.Segment).ToList()) });
        }

        private bool CompteExists(int id)
        {
            return _context.Comptes.Any(e => e.CompteID == id);
        }
    }
}
