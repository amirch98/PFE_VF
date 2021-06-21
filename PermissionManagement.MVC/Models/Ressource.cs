using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
    public enum SRegion
    {
        Nord,Centre,Sud
    }
    public class Ressource 
    {
        public int RessourceID { get; set; }
        [Display(Name =("Nom"))]
        public string RName { get; set; }
        [Display(Name =("Prenom"))]
        public string RPrenom { get; set; }
        public SRegion? SRegion { get; set; }
        public string Ville { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Entree { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Sortie { get; set; }
    }

}
