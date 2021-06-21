using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Data;
using PermissionManagement.MVC.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using PermissionManagement.MVC.Controllers;
using static PermissionManagement.MVC.Helper;
using Microsoft.AspNetCore.Authorization;

namespace pfe.Controllers
{
    [Authorize(Roles = "Responsable_Marketing,Manager")]
    public class PlanMedicalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanMedicalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlanMedicals
        public async Task<IActionResult> Index()
        {
            var pfeContext = _context.PlanMedicals.Include(p => p.Brand).Include(p => p.Segment).Include(p => p.Produit);
            return View(await pfeContext.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandNom");
                ViewData["SegmentID"] = new SelectList(_context.Segments, "SegmentID", "SegmentID");
                return View(new PlanMedical());
            }
            else
            {
                var planMedical = await _context.PlanMedicals.FindAsync(id);
                if (planMedical == null)
                {
                    return NotFound();
                }
                ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandNom", planMedical.BrandID);
                ViewData["SegmentID"] = new SelectList(_context.Segments, "SegmentID", "SegmentID", planMedical.SegmentID);
                return View(planMedical);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("PlanMedicalID,PName,date_debut,date_fin,Frequence,E_Proposed,BrandID,SegmentID")] PlanMedical planMedical)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _context.Add(planMedical);
                    var change_Log = new Change_Log
                    {
                        Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "PlanMedical", planMedical.PlanMedicalID, planMedical.PName)
                    };

                    _context.Change_Log.Add(change_Log);
                    await _context.SaveChangesAsync();

                    double fin = (planMedical.date_fin - planMedical.date_debut).TotalDays;
                    var p_marketing = new PlanMarketing
                    {
                        Name = planMedical.PName + "#Marketing",
                        Date_debut = planMedical.date_fin.AddDays(1),
                        Date_fin = planMedical.date_fin.AddDays(fin + 1),
                        PlanMedicalID = planMedical.PlanMedicalID
                    };


                    _context.PlanMarketings.Add(p_marketing);
                    var change_Log1 = new Change_Log
                    {
                        Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "PlanMarketing", p_marketing.PlanMarketingID, p_marketing.Name)
                    };
                    _context.Change_Log.Add(change_Log1);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(planMedical);
                        var change_Log = new Change_Log
                        {
                            Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "modifiée", "PlanMedical", planMedical.PlanMedicalID, planMedical.PName)
                        };

                        _context.Change_Log.Add(change_Log);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PlanMedicalExists(planMedical.PlanMedicalID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.PlanMedicals.ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", planMedical) });
        }

        // GET: PlanMedicals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planMedical = await _context.PlanMedicals
                .Include(p => p.Brand)
                .Include(p => p.Segment)
                .FirstOrDefaultAsync(m => m.PlanMedicalID == id);
            if (planMedical == null)
            {
                return NotFound();
            }

            return View(planMedical);
        }

        // POST: PlanMedicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id )
        {
            var planMedical = await _context.PlanMedicals.FindAsync(id);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "supprimée", "PlanMedical", planMedical.PlanMedicalID, planMedical.PName)
            };

            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.PlanMedicals.ToList()) });
        }

        private bool PlanMedicalExists(int id)
        {
            return _context.PlanMedicals.Any(e => e.PlanMedicalID == id);
        }

        //GET: CIBLES
        public async Task<IActionResult> Cibles(int id)
        {
            var targets = _context.PlanTargets.Include(p => p.Compte).Include(p => p.Contact).Where(p => p.PlanMedicalID == id);

            return View(await targets.ToListAsync());
        }

        //GET: GENERATE
        public IActionResult Generate()
        {
            {
                return View();
            }
        }



        //POST: GENERATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Generate(int id, PlanTarget plantarget)
        {
            var affectations = _context.Affectation;
            var segments = _context.Segments;
            var contacts = _context.Contacts;
            var consents = _context.Consents;
            var comptes = _context.Comptes;
            var planMedical = _context.PlanMedicals.First(p => p.PlanMedicalID == id);
            var planMarketing = _context.PlanMarketings.First(p => p.PlanMedicalID == planMedical.PlanMedicalID);

            var type =
                     (from segment in segments
                     where segment.SegmentID == planMedical.SegmentID
                     select segment.SType).ToArray();

            if (planMedical.SegmentID.HasValue == true && planMedical.BrandID.HasValue == false)
            {
                if (type[0] == 0)
                {
                    var con =
                    (from contact in contacts
                     where planMedical.SegmentID == contact.SegmentID
                     select contact).ToArray();

                    for (int i = 0; i < con.Length; i++)
                    {
                        var tar = new PlanTarget
                        {
                            ContactID = con[i].ContactID,
                            PlanMedicalID = id,
                            PlanMarketingID = planMarketing.PlanMarketingID
                        };
                        _context.PlanTargets.Add(tar);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    var com =
                        from compte in comptes
                        where planMedical.SegmentID == compte.SegmentID
                        select compte.CompteID;

                    int[] comm = com.ToArray();

                    for (int i = 0; i < comm.Length; i++)
                    {
                        var concom =
                            from affectation in affectations
                            where affectation.CompteID == comm[i]
                            select affectation.ContactID;

                        int[] concoom = concom.ToArray();

                        if (concoom.Length == 0)
                        {
                            var tarr = new PlanTarget
                            {
                                CompteID = comm[i],
                                PlanMedicalID = id,
                                PlanMarketingID = planMarketing.PlanMarketingID
                            };
                            _context.PlanTargets.Add(tarr);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            for (int j = 0; j < concoom.Length; j++)
                            {
                                var tarr = new PlanTarget
                                {
                                    ContactID = concoom[j],
                                    PlanMedicalID = id,
                                    CompteID = comm[i],
                                    PlanMarketingID = planMarketing.PlanMarketingID
                                };
                                _context.PlanTargets.Add(tarr);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            else if (planMedical.SegmentID.HasValue == false && planMedical.BrandID.HasValue == true)
            {
                var bran =
                  from consent in consents
                  where consent.BrandID == planMedical.BrandID && consent.Statut == "Accepté"
                  select consent.ContactID;

                int[] consentcon = bran.ToArray();

                for (int i = 0; i < consentcon.Length; i++)
                {
                    var compteres =
                            (from affectation in affectations
                             where affectation.ContactID == consentcon[i] && affectation.Contact.Statut == "Active"
                             select affectation.CompteID).ToArray();

                    if (compteres.Length == 0)
                    {
                        var target = new PlanTarget
                        {
                            PlanMedicalID = id,
                            ContactID = consentcon[i],
                            PlanMarketingID = planMarketing.PlanMarketingID

                        };
                        _context.PlanTargets.Add(target);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        for (int j = 0; j < compteres.Length; j++)
                        {
                            var target = new PlanTarget
                            {
                                PlanMedicalID = id,
                                ContactID = consentcon[i],
                                CompteID = compteres[j],
                                PlanMarketingID = planMarketing.PlanMarketingID
                            };
                            _context.PlanTargets.Add(target);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            else
            {
                if (type[0] == 0)
                {
                    int[] res =
                        (from conseg in contacts
                         join conbrand in consents on conseg.ContactID equals conbrand.ContactID into resfs
                         from resf in resfs
                         where resf.BrandID == planMedical.BrandID && resf.Contact.SegmentID == planMedical.SegmentID && resf.Statut == "Accepté" && resf.Contact.Statut == "Active"
                         select resf.ContactID).ToArray();

                    for (int j = 0; j < res.Length; j++)
                    {
                        var target = new PlanTarget
                        {
                            PlanMedicalID = id,
                            ContactID = res[j],
                            PlanMarketingID = planMarketing.PlanMarketingID
                        };
                        _context.PlanTargets.Add(target);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    int[] con_sb =
                        (from consent in consents
                         where consent.BrandID == planMedical.BrandID && consent.Statut == "Accepté" && consent.Contact.Statut == "Active"
                         select consent.ContactID).ToArray();

                    for (int i = 0; i < con_sb.Length; i++)
                    {
                        var compt_con =
                                from affectation in affectations
                                where affectation.ContactID == con_sb[i] && affectation.Compte.SegmentID == planMedical.SegmentID
                                select new { affectation.CompteID, affectation.ContactID };

                        var target = new PlanTarget
                        {
                            PlanMedicalID = id,
                            ContactID = compt_con.FirstOrDefault().ContactID,
                            CompteID = compt_con.FirstOrDefault().CompteID,
                            PlanMarketingID = planMarketing.PlanMarketingID
                        };
                        _context.PlanTargets.Add(target);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "généré cibles pour", "PlanMedical", planMedical.PlanMedicalID, planMedical.PName)
            };

            _context.Change_Log.Add(change_Log);
            planMedical.Targets_Generated = true;
            _context.PlanMedicals.Update(planMedical);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.PlanMedicals.ToList()) });
        }

        //GET : Ajout Produit
        public IActionResult Ajout_Produit(int id)
        {
            var plan = _context.PlanMedicals.First(p => p.PlanMedicalID == id);
            ViewData["ProduitID"] = new SelectList(_context.Produits.Where(p => p.BrandID == plan.BrandID).ToList(), "ProduitID", "ProduitNom");
            return View();
        }

        //GET : Ressource for contact
        private int[] Get_Ressource(int? contact)
        {
            var ressources = _context.Ressources;
            Contact con = _context.Contacts.First(c => c.ContactID == contact);

            return (from ress in ressources
                    where ress.Ville.Contains(con.Ville)
                    select ress.RessourceID).ToArray();
        }

        //POST : Ajout Produit
        [HttpPost]
        public async Task<IActionResult> Ajout_Produit(int id, [Bind("PlanMedicalID,PName,date_debut,date_fin,BrandID,Frequence,E_Proposed,ProduitID,SegmentID,Resultat")] PlanMedical planMedical, Activite activite)
        {
            var targets = _context.PlanTargets;
            int?[] contacts = (
                from contact in targets
                where contact.PlanMedicalID == id
                select contact.ContactID).ToArray();

            PlanMedical plan = _context.PlanMedicals.First(p => p.PlanMedicalID == id);
            var produit = _context.Produits.FirstOrDefault(p => p.ProduitID == planMedical.ProduitID);

            if (ModelState.IsValid)
            {
                if (plan.ProduitID.HasValue)
                {
                    if (contacts.Length * planMedical.E_Proposed * planMedical.Frequence > produit.Stock_Theorique)
                    {
                        ViewBag.Message = "Stock insuffisant! Alimentez le..";
                        ViewData["ProduitID"] = new SelectList(_context.Produits, "ProduitID", "ProduitNom", planMedical.ProduitID);
                        return View(planMedical);
                    }
                }
                else
                {
                    plan.Frequence = planMedical.Frequence;
                    plan.E_Proposed = planMedical.E_Proposed;
                    plan.ProduitID = planMedical.ProduitID;
                    _context.PlanMedicals.Update(plan);
                    await _context.SaveChangesAsync();

                    int f = plan.Frequence;
                    double days = (plan.date_fin.Date - plan.date_debut.Date).Days;
                    double periode = days / f;

                    int r = 0;
                    for (int i = 0; i < contacts.Length; i++)
                    {
                        int? ressource;
                        if (Get_Ressource(contacts[i]).Length == 0)
                            ressource = null;
                        else
                            ressource = Get_Ressource(contacts[i])[r];

                        var act = new Activite
                        {
                            Statut = 0,
                            De = plan.date_debut,
                            À = plan.date_debut.AddDays(periode),
                            Type = "Visite",
                            PlanMedicalID = id,
                            ContactID = (int)contacts[i],
                            RessourceID = ressource
                        };

                        _context.Add(act);

                        if (plan.ProduitID.HasValue)
                        {
                            var alloca = new Allocation
                            {
                                A_Type = "Contact",
                                ContactID = contacts[i],
                                ProduitID = (int)plan.ProduitID,
                                A_CreatedOn = DateTime.Now,
                                A_To = act.De,
                                Quantity = plan.E_Proposed,
                                A_Statut = (A_Statut)1
                            };



                            _context.Allocation.Add(alloca);
                            produit.Stock_Theorique -= alloca.Quantity;
                            _context.Produits.Update(produit);
                            await _context.SaveChangesAsync();

                            var act_final = _context.Activites.First(a => a.ActiviteID == act.ActiviteID);
                            act_final.AllocationID = alloca.AllocationID;
                            _context.Activites.Update(act_final);
                        }
                        await _context.SaveChangesAsync();


                        DateTime from = act.À.AddDays(1);
                        DateTime to = from.AddDays(periode - 1);

                        for (int j = 1; j < f; j++)
                        {
                            var act_suite = new Activite
                            {
                                Statut = 0,
                                De = from,
                                À = to,
                                Type = "Visite",
                                PlanMedicalID = id,
                                ContactID = (int)contacts[i],
                                RessourceID = ressource
                            };
                            _context.Activites.Add(act_suite);

                            if (plan.ProduitID.HasValue)
                            {

                                var alloca_suite = new Allocation
                                {
                                    A_Type = "Contact",
                                    ContactID = contacts[i],
                                    ProduitID = (int)plan.ProduitID,
                                    A_CreatedOn = DateTime.Now,
                                    A_To = act_suite.De,
                                    Quantity = plan.E_Proposed,
                                    A_Statut = (A_Statut)1
                                };

                                _context.Allocation.Add(alloca_suite);
                                produit.Stock_Theorique -= alloca_suite.Quantity;
                                _context.Produits.Update(produit);
                                await _context.SaveChangesAsync();

                                var act_suite_final = _context.Activites.First(a => a.ActiviteID == act_suite.ActiviteID);
                                act_suite_final.AllocationID = alloca_suite.AllocationID;
                                _context.Activites.Update(act_suite_final);
                            }
                            await _context.SaveChangesAsync();

                            from = to.AddDays(1);
                            to = from.AddDays(periode - 1);
                        }

                        if (r >= Get_Ressource(contacts[i]).Length)
                            r = 0;
                        else
                            r++;
                    }
                }
                plan.Activities_Generated = true;
                _context.PlanMedicals.Update(plan);
                var change_Log = new Change_Log
                {
                    Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "généré activites pour", "PlanMedical", plan.PlanMedicalID, plan.PName)
                };

                _context.Change_Log.Add(change_Log);
                await _context.SaveChangesAsync();
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.PlanMedicals.ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "Ajout_Produit", planMedical) });
        }

        // GET : Activites
        public async Task<IActionResult> Activites(int id)
        {
            var activites = _context.Activites.Include(a => a.Contact).Include(a => a.PlanMedical).Where(a => a.PlanMedicalID == id);
            return View(await activites.ToListAsync());
        }
    }
}

