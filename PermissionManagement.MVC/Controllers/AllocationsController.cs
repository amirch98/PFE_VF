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

    public class AllocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Allocations
        public async Task<IActionResult> Index()
        {
            var pfeContext = _context.Allocation.Include(a => a.Produit).Include(a => a.Contact);
            return View(await pfeContext.ToListAsync());
        }

        // GET: Allocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocation = await _context.Allocation
                .Include(a => a.Produit)
                .Include(a => a.Contact)
                .FirstOrDefaultAsync(m => m.AllocationID == id);
            if (allocation == null)
            {
                return NotFound();
            }

            return View(allocation);
        }


        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            { ViewData["ContactID"] = new SelectList(_context.Contacts, "ContactID", "LastName");
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ProduitID", "ProduitNom");
            return View(new Allocation());
        } else
            {

                var allocation = await _context.Allocation.FindAsync(id);

                if (allocation == null)
                {
                    return NotFound();
                }
                ViewData["ContactID"] = new SelectList(_context.Contacts, "ContactID", "LastName", allocation.ContactID);
                ViewData["ProduitID"] = new SelectList(_context.Produits, "ProduitID", "ProduitNom", allocation.ProduitID);
                return View(allocation);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> AddOrEdit(int id, [Bind("AllocationID,ContactID,CompteID,ProduitID,A_To,A_CreatedOn,Quantity,A_Statut,A_Type")] Allocation allocation)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    var produitt = _context.Produits.First(a => a.ProduitID == allocation.ProduitID);
                 
                    
                    if (allocation.Quantity <= 0)
                    {
                        ModelState.AddModelError("Quantity", "La quantité ne doit pas être null");
                        ViewData["ContactID"] = new SelectList(_context.Contacts, "ContactID", "LastName", allocation.ContactID);
                        ViewData["ProduitID"] = new SelectList(_context.Produits, "ProduitID", "ProduitNom", allocation.ProduitID);
                        return View(allocation);
                    }
                     if (allocation.Quantity > produitt.Stock_Theorique)
                    {
                        ModelState.AddModelError("Quantity", "La quantité donner est superieur au stock");
                        ViewData["ContactID"] = new SelectList(_context.Contacts, "ContactID", "LastName", allocation.ContactID);
                        ViewData["ProduitID"] = new SelectList(_context.Produits, "ProduitID", "ProduitNom", allocation.ProduitID);
                        return View(allocation);
                    }
                  
                        allocation.A_CreatedOn = DateTime.Now;
                        _context.Add(allocation);
                        produitt.Stock_Theorique -= allocation.Quantity;
                        _context.Update(produitt);
                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "Allocation", allocation.AllocationID, allocation.A_Type)
                        };
                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                    
                }
                //Update
                else
                {
                    var alloca = _context.Allocation.First(al => al.AllocationID == id);
                    var produit = _context.Produits.First(a => a.ProduitID == alloca.ProduitID);


                    try
                    {
                        if (allocation.A_Statut == 0)
                        {
                            produit.Stock_Physique -= alloca.Quantity;
                            _context.Update(produit);
                            _context.Update(alloca);
                            var change_Log = new Change_Log
                            {
                                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "modifiée", "Allocation", allocation.AllocationID, allocation.A_Type)
                            };
                            _context.Change_Log.Add(change_Log);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            _context.Update(alloca);
                            var change_Log = new Change_Log
                            {
                                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "modifiée", "Allocation", allocation.AllocationID, allocation.A_Type)
                            };
                            _context.Change_Log.Add(change_Log);
                            await _context.SaveChangesAsync();
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AllocationExists(allocation.AllocationID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                    return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.Allocation.Include(a => a.Produit).Include(a => a.Contact).ToList()) });
                }
                return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", allocation) });
            }

        

      
       
        // GET: Allocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocation = await _context.Allocation
                .Include(a => a.Produit)
                .Include(a => a.Contact)
                .FirstOrDefaultAsync(m => m.AllocationID == id);
            if (allocation == null)
            {
                return NotFound();
            }

            return View(allocation);
        }

        // POST: Allocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allocation = await _context.Allocation.FindAsync(id);
            _context.Allocation.Remove(allocation);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "supprimée", "Allocation", allocation.AllocationID, allocation.A_Type)
            };
            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.Allocation.Include(a => a.Produit).Include(a => a.Contact).ToList()) });
        }

        private bool AllocationExists(int id)
        {
            return _context.Allocation.Any(e => e.AllocationID == id);
        }
    }
}
