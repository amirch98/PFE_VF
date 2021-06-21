using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Data;
using PermissionManagement.MVC.Models;
using static PermissionManagement.MVC.Helper;
namespace PermissionManagement.MVC.Controllers
{
    [Authorize(Roles = "Responsable_Comercial,Manager")]

    public class RessourcesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RessourcesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ressources
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ressources.ToListAsync());
        }

        // GET: Ressources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ressource = await _context.Ressources
                .FirstOrDefaultAsync(m => m.RessourceID == id);
            if (ressource == null)
            {
                return NotFound();
            }

            return View(ressource);
        }


        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {

                return View(new Ressource());

            }
            else
            {
                var ressource = await _context.Ressources.FindAsync(id);
                if (ressource == null)
                {
                    return NotFound();
                }
                return View(ressource);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("RessourceID,RName,RPrenom,pays,SRegion,Vill,Entree,Sortie")] Ressource ressource , IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    ressource.Ville = form["ville"];
                    _context.Add(ressource);
                    var change_Log = new Change_Log
                    {
                        Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "Ressource", ressource.RessourceID, ressource.RName)
                    };

                    _context.Change_Log.Add(change_Log);
                    await _context.SaveChangesAsync();
                  
                }
                
            
                //Update
                else
                {

                    try
                    {
                        _context.Update(ressource);
                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "modifiée", "Ressource", ressource.RessourceID, ressource.RName)
                        };

                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RessourceExists(ressource.RessourceID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                }
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.Ressources.ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", ressource) });
        }



        // GET: Ressources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ressource = await _context.Ressources
                .FirstOrDefaultAsync(m => m.RessourceID == id);
            if (ressource == null)
            {
                return NotFound();
            }

            return View(ressource);
        }

        // POST: Ressources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id )
        {
            var ressource = await _context.Ressources.FindAsync(id);
            _context.Ressources.Remove(ressource);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "supprimée", "Ressource", ressource.RessourceID, ressource.RName)
            };

            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.Ressources.ToList()) });
        }

        private bool RessourceExists(int id)
        {
            return _context.Ressources.Any(e => e.RessourceID == id);
        }

        // GET: Activites
        public async Task<IActionResult> Index1(int? id)
        {
            var pfeContext = _context.Activites.Include(a => a.Allocation).Include(a => a.Contact).Include(a => a.PlanMarketing).Include(a => a.PlanMedical).Include(a => a.Ressource).Where(a => a.RessourceID == id);
            return View(await pfeContext.ToListAsync());
        }
    }
}
