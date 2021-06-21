using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using DnsClient;
using KellermanSoftware.NetEmailValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Data;
using PermissionManagement.MVC.Models;
using static PermissionManagement.MVC.Helper;
using static PermissionManagement.MVC.APIResult;


namespace PermissionManagement.MVC.Controllers
{
    [Authorize(Roles = "Manager")]

    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var pfeContext = _context.Contacts.Include(c => c.Segment);
            return View(await pfeContext.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewData["SegmentID"] = new SelectList(_context.Segments.Where(s => s.SType == 0).ToList(), "SegmentID", "SName");
                return View(new Contact());
            }
            else
            {
                var contact = await _context.Contacts.FindAsync(id);
                if (contact == null)
                {
                    return NotFound();
                }
                ViewData["SegmentID"] = new SelectList(_context.Segments, "SegmentID", "SName", contact.SegmentID);
                return View(contact);
            }
        }

        public bool validmail (string mail)
        {
            Result myResult;
            EmailValidation valid = new EmailValidation();

            myResult = valid.ValidEmail(mail);
            if (!myResult.IsValid) {
                return false;
            }
            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ContactID,Title,Name,LastName,Tel,Email,Statut,SegmentID")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    var emails = _context.Contacts.Where(c => c.Email == contact.Email);
                    if (emails.Any() == true)
                    {
                        ModelState.AddModelError("Email", "email exists!");
                        ViewData["SegmentID"] = new SelectList(_context.Segments.Where(s => s.SType == 0).ToList(), "SegmentID", "SName", contact.SegmentID);

                        return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", contact) });

                    }
                    if (Checkemail(contact.Email) == false)
                    {
                        ModelState.AddModelError("Email", "invalid email!");
                        ViewData["SegmentID"] = new SelectList(_context.Segments.Where(s => s.SType == 0).ToList(), "SegmentID", "SName", contact.SegmentID);
                        return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", contact) });

                    }
                    _context.Add(contact);
                    var change_Log = new Change_Log
                    {
                        Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "Contact", contact.ContactID, contact.LastName + " " + contact.Name)
                    };
                    _context.Change_Log.Add(change_Log);
                    await _context.SaveChangesAsync();
                }
                //Update
                else
                {
                    
                }
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.Contacts.Include(c => c.Segment).ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", contact) });
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .Include(c => c.Segment)
                .FirstOrDefaultAsync(m => m.ContactID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            contact.Statut = "Deleted";
            _context.Contacts.Update(contact);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "supprimée", "Contact", contact.ContactID, contact.LastName + " " + contact.Name)
            };
            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.Contacts.ToList()) });
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactID == id);
        }

        //GET : Active/desactive
        public async Task<IActionResult> Changer_S(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        //POST : Active/desactive
        [HttpPost]
        public async Task<IActionResult> Changer_S(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact.Statut == "Desactived")
            {
                contact.Statut = "Active";
            }
            else if (contact.Statut == "Active")
            {
                contact.Statut = "Desactived";
            }

            _context.Contacts.Update(contact);
            var change_Log = new Change_Log
            {
                Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, contact.Statut, "Contact", contact.ContactID, contact.LastName + " " + contact.Name)
            };
            _context.Change_Log.Add(change_Log);
            await _context.SaveChangesAsync();

            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.Contacts.ToList()) });
        }

        // GET: Affectations/Create
        public IActionResult CreateA()
        {
            ViewData["CompteID"] = new SelectList(_context.Comptes, "CompteID", "AccountName");
            return View();
        }

        // POST: Affectations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateA(int id, [Bind("AffectationID,debut,fin,CompteID,ContactID")] Affectation affectation)
        {
            if (ModelState.IsValid)
            {
                var contact = _context.Contacts.FirstOrDefault(x => x.ContactID == id);

                affectation.ContactID = id;
                _context.Add(affectation);
                var change_Log = new Change_Log
                {
                    Log = ChangeLog.GetUserLog(HttpContext.User.Identity.Name, "ajoutée", "Contact", id, contact.LastName + " " + contact.Name)
                };
                _context.Change_Log.Add(change_Log);
                await _context.SaveChangesAsync();
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.Contacts.ToList()) });

            }
            ViewData["CompteID"] = new SelectList(_context.Comptes, "CompteID", "AccountName", affectation.CompteID);
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "CreateA", affectation) });
        }

        // GET: Affectations
        public async Task<IActionResult> IndexA()
        {
            var pfeContext = _context.Affectation.Include(a => a.Compte).Include(a => a.Contact);
            return View(await pfeContext.ToListAsync());
        }

        Task<bool> IsValidAsync(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                var host = mailAddress.Host;
                return CheckDnsEntriesAsync(host);
            }
            catch (FormatException)
            {
                return Task.FromResult(false);
            }
        }

        [Obsolete]
        async Task<bool> CheckDnsEntriesAsync(string domain)
        {
            try
            {
                var lookup = new LookupClient
                {
                    Timeout = TimeSpan.FromSeconds(5)
                };
                var result = await lookup.QueryAsync(domain, QueryType.ANY).ConfigureAwait(false);

                var records = result.Answers.Where(record => record.RecordType == DnsClient.Protocol.ResourceRecordType.A ||
                                                             record.RecordType == DnsClient.Protocol.ResourceRecordType.AAAA ||
                                                             record.RecordType == DnsClient.Protocol.ResourceRecordType.MX);
                return records.Any();
            }
            catch (DnsResponseException)
            {
                return false;
            }
        }
    }
}
