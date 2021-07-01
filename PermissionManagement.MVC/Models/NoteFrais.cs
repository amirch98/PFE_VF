using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
   
    public class NoteFrais
    {
      
        public int NoteFraisID { get; set; }
        [Display(Name = ("Ammount"))]

        public double montant { get; set; }
        [Display(Name = ("Type"))]

        public string type { get; set; }
        [Display(Name = ("Statut"))]

        public string statut { get; set; }
    } 
}
