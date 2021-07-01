using System;
using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.MVC.Models
{
    public class Consent 
    {
        public int ConsentID { get; set; }
        public string Title { get; set; }
        [Display(Name = ("Contact"))]

        public int ContactID { get; set; }
        [Display(Name = ("Brand"))]

        public int BrandID { get; set; }
        [Display(Name = ("Created On"))]

        public DateTime C_CreatedOn { get; set; } = DateTime.Now;
        [Display(Name = ("By"))]

        public string C_CreatedBy { get; set; }
        public string Statut { get; set; } = "En Cours";

        public Contact Contact { get; set; }
        public Brand Brand { get; set; }


    }
}
