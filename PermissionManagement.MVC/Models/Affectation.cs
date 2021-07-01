using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
    public class Affectation
    {
        public int AffectationID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = ("Start"))]

        public DateTime debut { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = ("End"))]

        public DateTime? fin { get; set; }
        [Display(Name = ("Compte"))]

        public int CompteID { get; set; }
        [Display(Name = ("Contact"))]

        public int ContactID { get; set; }

        public Compte Compte { get; set; }
        public Contact Contact { get; set; }

    }
}
