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

    public class PlanMarketingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanMarketingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlanMarketings
        public async Task<IActionResult> Index()
        {
            var pfeContext = _context.PlanMarketings.Include(p => p.PlanMedical);
            return View(await pfeContext.ToListAsync());
        }

        // GET: PlanMarketings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planMarketing = await _context.PlanMarketings
                .Include(p => p.PlanMedical)
                .FirstOrDefaultAsync(m => m.PlanMarketingID == id);
            if (planMarketing == null)
            {
                return NotFound();
            }

            return View(planMarketing);
        }

        // POST: PlanMarketings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id )
        {
            var planMarketing = await _context.PlanMarketings.FindAsync(id);
            _context.PlanMarketings.Remove(planMarketing);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "supprimée", "PlanMarketing", planMarketing.PlanMarketingID, planMarketing.Name)
            };
           
            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.PlanMarketings.Include(p => p.PlanMedical).ToList()) });
        }

        private bool PlanMarketingExists(int id)
        {
            return _context.PlanMarketings.Any(e => e.PlanMarketingID == id);
        }

        //GET: CIBLES
        public async Task<IActionResult> Cibles(int id)
        {
            var targets = _context.PlanTargets.Include(p => p.Compte).Include(p => p.Contact).Where(p => p.PlanMarketingID == id);

            return View(await targets.ToListAsync());
        }

        //GET : Activites
        public IActionResult Generer_A(int id)
        {
            return View();
        }

        private static Activite get_Activite(DateTime debut, DateTime fin, string type, int key, int? contact, int? ressource, int statut)
        {
            var act = new Activite
            {
                Statut = (Act_Statut?)statut,
                De = debut,
                À = fin,
                Type = type,
                PlanMarketingID = key,
                ContactID = (int)contact,
                RessourceID = ressource
            };
            return (act);
        }

        //GET : Ressource for contact
        private int[] Get_Ressource(int? contact)
        {
            var ressources = _context.Ressources;
            var con = _context.Contacts.First(c => c.ContactID == contact);
            var res =
                (from ress in ressources
                 where ress.Ville.Contains(con.Ville)
                 select ress.RessourceID).ToArray();

            return res;
        }

        //POST : Activites
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Generer_A(int id, PlanMarketing p_marketing, IFormCollection form)
        {
            var targets = _context.PlanTargets;
            var contacts = (
                from contact in targets
                where contact.PlanMarketingID == id
                select contact).ToArray();



            var plan = _context.PlanMarketings.First(p => p.PlanMarketingID == id);

            if (ModelState.IsValid)
            {
                plan.Frequence = p_marketing.Frequence;
                _context.PlanMarketings.Update(plan);
                await _context.SaveChangesAsync();

                double days = (plan.Date_fin - plan.Date_debut).Days;
                int f = (int)plan.Frequence;
                double periode = days / f;
                string[] ty = form["Type"].ToArray();
                double p_type = periode / ty.Length;

                if (contacts.Length == 0)
                {
                    ViewBag.Message = "Ce plan n'a pas des cibles";
                }
                else
                {
                    int r = 0;

                    for (int i = 0; i < contacts.Length; i++)
                    {
                        int? ressource = null;
                        if (Get_Ressource(contacts[i].ContactID).Length != 0)
                            ressource = Get_Ressource(contacts[i].ContactID)[r];

                        DateTime debut = plan.Date_debut;
                        DateTime fin = plan.Date_debut.AddDays(p_type);

                        foreach (string item in ty)
                        {
                            var act = get_Activite(debut, fin, item, id, contacts[i].ContactID, ressource, 0);
                            _context.Activites.Add(act);
                            await _context.SaveChangesAsync();

                            debut = fin.AddDays(1);
                            fin = debut.AddDays(p_type - 1);
                        }

                        for (int j = 1; j < f; j++)
                        {
                            foreach (string item in ty)
                            {
                                var act_suite = get_Activite(debut, fin, item, id, contacts[i].ContactID, ressource, 0);
                                _context.Activites.Add(act_suite);
                                await _context.SaveChangesAsync();

                                debut = fin.AddDays(1);
                                fin = debut.AddDays(p_type - 1);
                            }
                        }

                        if (r >= Get_Ressource(contacts[i].ContactID).Length)
                            r = 0;
                        else
                            r++;
                    }
                    plan.Activities_Generated = true;
                    _context.PlanMarketings.Update(plan);
                    var change_Log = new Change_Log
                    {
                        Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "généré activites", "PlanMarketing", plan.PlanMarketingID, plan.Name)
                    };

                    _context.Change_Log.Add(change_Log);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        // GET : Activites
        public async Task<IActionResult> Index_A(int id)
        {
            var activites = _context.Activites.Include(a => a.Contact).Include(a => a.PlanMarketing).Where(a => a.PlanMarketingID == id);
            return View(await activites.ToListAsync());
        }

    }
}
