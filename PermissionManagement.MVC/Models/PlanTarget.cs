using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
    public class PlanTarget
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlanTargetID { get; set; }
        public int PlanMedicalID { get; set; }
        public int PlanMarketingID { get; set; }
        public int? CompteID { get; set; }
        public int? ContactID { get; set; }


        public PlanMarketing PlanMarketing { get; set; }
        public PlanMedical PlanMedical { get; set; }
        public Compte Compte { get; set; }
        public Contact Contact { get; set; }
    }
}
