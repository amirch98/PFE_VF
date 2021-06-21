using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
   
    public enum Axe
    {
        Geographiques, Demographiques, Psychographiques, Comportementale
    }
    
    public enum SType
    {
        Contact, Compte
    }
  
    public class Segment
    {
        public int SegmentID { get; set; }
        [Display(Name =("Nom"))]
        public string SName { get; set; }
        [Range (1,100)]
        [Display(Name =("Rang"))]
        public int SRang { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Display(Name =("Type"))]
        public SType SType { get; set; }
        public Axe? Axe { get; set; }
        public string Raison { get; set; }

        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Compte> Comptes { get; set; }

    }
}
