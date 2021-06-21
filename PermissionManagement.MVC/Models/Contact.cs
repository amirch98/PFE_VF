using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
    
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactID { get; set; }
        [Display(Name = ("Titre"))]
        public string Title { get; set; }
        [Required]
        [Display(Name =("Nom"))]
        public string Name { get; set; }
        [Required]
        [Display(Name = ("Prénom"))]
        public string LastName { get; set; }
        [Display(Name = ("Téléphone"))]
        public int Tel { get; set; }
        public string Ville { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Statut { get; set; } = "Desactived";
        public int? SegmentID { get; set; }

        public Segment Segment { get; set; }

        public ICollection<Allocation> Allocations { get; set; }
        public ICollection<Consent> Consents { get; set; }
    }
}
