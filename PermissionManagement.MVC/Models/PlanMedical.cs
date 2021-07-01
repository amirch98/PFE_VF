using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
   
    public class PlanMedical
    {
        public int PlanMedicalID { get; set; }
        [Display(Name = ("Name"))]

        public string  PName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = ("Start"))]

        public DateTime date_debut { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = ("End"))]

        public DateTime date_fin { get; set; }
        [Display(Name = ("Brand"))]

        public int? BrandID { get; set; }
        [Display(Name = ("Segment"))]

        public int? SegmentID { get; set; }
        [Display(Name = ("Proposed"))]

        public int E_Proposed { get; set; }
        public int Frequence { get; set; }
        [Display(Name = ("Produit"))]

        public int? ProduitID { get; set; }
        public bool Targets_Generated { get; set; } = false;
        public bool Activities_Generated { get; set; } = false;

        public Produit Produit { get; set; }
        public Brand Brand { get; set; }
        public Segment Segment { get; set; }
    }
}
