using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
   
    public class NoteFrais
    {
      
        public int NoteFraisID { get; set; }
        public double montant { get; set; }
        public string type { get; set; }
        public string statut { get; set; }
    } 
}
