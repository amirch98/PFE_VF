using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Data;
using PermissionManagement.MVC.Models;

namespace PermissionManagement.MVC.Controllers
{
    public class Change_LogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Change_LogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Change_Log
        public async Task<IActionResult> Index()
        {
            return View(await _context.Change_Log.ToListAsync());
        }

       
    }
}
