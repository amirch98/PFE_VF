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

    public class PricesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prices.ToListAsync());
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .FirstOrDefaultAsync(m => m.PriceID == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

       

       
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
               
                return View(new Price());

            }
            else
            {
                var price = await _context.Prices.FindAsync(id);
                if (price == null)
                {
                    return NotFound();
                }
                return View(price);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("PriceID,PriceNom,Context,DateDebut,DateFin,Currency,Unit")] Price price)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    if (price.DateDebut < price.DateFin)
                    {
                        _context.Add(price);

                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "Liste de prix", price.PriceID, price.PriceNom)
                        };
                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                       
                    }
                    else
                    {
                        ModelState.AddModelError("DateDebut", "Date de debut doit etre infernieur au date fin");
                        return View(price);
                    }

                  
                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(price);
                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "modifiée", "Liste de prix", price.PriceID, price.PriceNom)
                        };
                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PriceExists(price.PriceID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                }
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.Prices.ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", price) });
        }


       
        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .FirstOrDefaultAsync(m => m.PriceID == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var price = await _context.Prices.FindAsync(id);
            _context.Prices.Remove(price);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "supprimée", "Liste de prix", price.PriceID, price.PriceNom)
            };
            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.Prices.ToList()) });
        }

        private bool PriceExists(int id)
        {
            return _context.Prices.Any(e => e.PriceID == id);
        }
    }
}
