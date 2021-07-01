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
        public string Title { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = ("Last Name"))]

        public string LastName { get; set; }
        [Display(Name = ("Phone"))]
        public int Tel { get; set; }
        [Display(Name = ("Adress"))]
        public string Ville { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Statut { get; set; } = "Desactived";
        [Display(Name = ("Segment"))]

        public int? SegmentID { get; set; }

        public Segment Segment { get; set; }

        public ICollection<Allocation> Allocations { get; set; }
        public ICollection<Consent> Consents { get; set; }
    }
}
