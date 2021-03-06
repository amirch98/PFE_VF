using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
    public enum CType
    {
        Clinique, Pharmacie, Cabinet, Distributeur
    }
    public class Compte
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompteID { get; set; }
        [Required]
        [Display(Name = ("Name"))]

        public string AccountName { get; set; }

        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Adress { get; set; }
        [Display(Name = ("Type"))]

        public CType? CType { get; set; }
        [Display(Name = ("Segment"))]

        public int? SegmentID { get; set; }
        
        public Segment Segment { get; set; }

    }
}
