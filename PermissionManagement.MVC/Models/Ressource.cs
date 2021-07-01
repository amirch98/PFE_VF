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
        [Display(Name =("Name"))]
        public string RName { get; set; }
        [Display(Name =("Last Name"))]
        public string RPrenom { get; set; }
        [Display(Name = ("Region"))]
        public SRegion? SRegion { get; set; }
        [Display(Name = ("Adress"))]
        public string Ville { get; set; }
        [Display(Name = ("Start"))]

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime Entree { get; set; }
        [Display(Name = ("End"))]

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Sortie { get; set; }
    }

}
