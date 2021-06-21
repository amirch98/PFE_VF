using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Data;
using PermissionManagement.MVC.Models;
using static PermissionManagement.MVC.Helper;

namespace PermissionManagement.MVC.Controllers
{
    [Authorize(Roles = "Manager")]

    public class ProduitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produits
        public async Task<IActionResult> Index()
        {
            var pfeContext = _context.Produits.Include(p => p.Brand).Include(p => p.Price);
            return View(await pfeContext.ToListAsync());
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandNom");
                ViewData["PriceID"] = new SelectList(_context.Prices.Where(p => p.DateFin > DateTime.Now), "PriceID", "PriceNom");
                return View(new Produit());
                
            }
            else
            {
                var produit = await _context.Produits.FindAsync(id);
                if (produit == null)
                {
                    return NotFound();
                }
                ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandNom", produit.BrandID);
                ViewData["PriceID"] = new SelectList(_context.Prices.Where(p => p.DateFin > DateTime.Now), "PriceID", "PriceNom", produit.PriceID);
                return View(produit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ProduitID,ProduitNom,Type,BrandID,Competitor,Unite,PriceID,PDescription,Stock_Theorique,Stock_Physique,CreatedBy")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    if (produit.Type == 0)
                    {
                        var brand = new Brand
                        {
                            BrandNom = produit.ProduitNom
                        };
                        _context.Brands.Add(brand);
                        await _context.SaveChangesAsync();
                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "Brand", brand.BrandID, brand.BrandNom)
                        };
                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        produit.Stock_Theorique = produit.Stock_Physique;
                        _context.Produits.Add(produit);
                        await _context.SaveChangesAsync();
                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "Produit", produit.ProduitID, produit.ProduitNom)
                        };
                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                    }

                    ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandNom", produit.BrandID);
                    ViewData["PriceID"] = new SelectList(_context.Prices.Where(p => p.DateFin > DateTime.Now), "PriceID", "PriceNom", produit.PriceID);

                }
                //Update
                else
                {
                    try
                    {

                        _context.Update(produit);
                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "modifiée", "Produit", produit.ProduitID, produit.ProduitNom)
                        };
                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProduitExists(produit.ProduitID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.Produits.ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", produit) });
        }


        // GET: Produits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.Brand)
                .Include(p => p.Price)
                .FirstOrDefaultAsync(m => m.ProduitID == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            _context.Produits.Remove(produit);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "suprrimée", "Produit", produit.ProduitID, produit.ProduitNom)
            };
            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.Produits.ToList()) });
        }

        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.ProduitID == id);
        }
    }
}
