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

namespace PermissionManagement.MVC.Controllers
{
    [Authorize(Roles = "Responsable_Marketing,Manager")]

    public class ActivitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Activites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Activites.Include(a => a.Allocation).Include(a => a.Contact).Include(a => a.PlanMarketing).Include(a => a.PlanMedical).Include(a => a.Ressource);
            return View(await applicationDbContext.ToListAsync());
        }

        
    }
}
