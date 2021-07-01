using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
    public enum Act_Statut
    {
        Crée, Effectuée, Annulée
    }
    public class Activite
    {
        public int ActiviteID { get; set; }
        [Display(Name = ("Plan Medical"))]

        public int? PlanMedicalID { get; set; }
        [Display(Name = ("Plan Marketing"))]

        public int? PlanMarketingID { get; set; }

        public string Type { get; set; }
        [Display(Name = ("Contact"))]

        public int ContactID { get; set; }
        [Display(Name = ("From"))]

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime De { get; set; }
        [Display(Name = ("To"))]

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime À { get; set; }

        public int? AllocationID { get; set; }
        public int? Ratings { get; set; }
        [Display(Name = ("Ressource"))]

        public int? RessourceID { get; set; }
        public Act_Statut? Statut { get; set; }


        public Ressource Ressource { get; set; }
        public PlanMarketing PlanMarketing { get; set; }
        public PlanMedical PlanMedical { get; set; }
        public Contact Contact { get; set; }
        public Allocation Allocation { get; set; }
    }
}
