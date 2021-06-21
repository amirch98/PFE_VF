using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Models;
using System;

namespace PermissionManagement.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<Activite> Activites { get; set; }
        public DbSet<Affectation> Affectation { get; set; }
        public DbSet<Allocation> Allocation { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Consent> Consents { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<NoteFrais> NoteFrais { get; set; }
        public DbSet<PlanMarketing> PlanMarketings { get; set; }
        public DbSet<PlanMedical> PlanMedicals { get; set; }
        public DbSet<PlanTarget> PlanTargets { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<Change_Log> Change_Log { get; set; }

    }
}