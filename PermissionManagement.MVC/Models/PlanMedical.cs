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
        public string  PName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date_debut { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date_fin { get; set; }
        public int? BrandID { get; set; }
        public int? SegmentID { get; set; }
        public int E_Proposed { get; set; }
        public int Frequence { get; set; }
        public int? ProduitID { get; set; }
        public bool Targets_Generated { get; set; } = false;
        public bool Activities_Generated { get; set; } = false;

        public Produit Produit { get; set; }
        public Brand Brand { get; set; }
        public Segment Segment { get; set; }
    }
}
