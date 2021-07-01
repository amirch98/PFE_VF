using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{

    public class PlanMarketing
    {
        public int PlanMarketingID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = ("Start"))]

        public DateTime Date_debut { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = ("End"))]

        public DateTime Date_fin { get; set; }
        [Display(Name = ("Plan Medical"))]

        public int PlanMedicalID { get; set; }
        public int? Frequence { get; set; }
        public bool Activities_Generated { get; set; } = false;

        public PlanMedical PlanMedical { get; set; }
    }
}
