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
    [Authorize(Roles = "Responsable_Marketing,Manager")]

    public class SegmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SegmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Segments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Segments.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Segment());
            else
            {
                var segment = await _context.Segments.FindAsync(id);
                if (segment == null)
                {
                    return NotFound();
                }
                return View(segment);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("SegmentID,SName,SRang,Description,SType,Axe,Raison")] Segment segment, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    segment.Raison = form["rais"];
                    _context.Add(segment);
                    var change_Log = new Change_Log
                    {
                        Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "Segment", segment.SegmentID, segment.SName)
                    };

                    _context.Change_Log.Add(change_Log);
                    await _context.SaveChangesAsync();
                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(segment);
                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "modifiée", "Segment", segment.SegmentID, segment.SName)
                        };

                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SegmentExists(segment.SegmentID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.Segments.ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", segment) });
        }

        // GET: Segments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments
                .FirstOrDefaultAsync(m => m.SegmentID == id);
            if (segment == null)
            {
                return NotFound();
            }

            return View(segment);
        }

        // POST: Segments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id )
        {
            var segment = await _context.Segments.FindAsync(id);
            _context.Segments.Remove(segment);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "supprimée", "Segment", segment.SegmentID, segment.SName)
            };

            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.Segments.ToList()) });
        }

        private bool SegmentExists(int id)
        {
            return _context.Segments.Any(e => e.SegmentID == id);
        }
    }
}
