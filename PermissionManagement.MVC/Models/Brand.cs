using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionManagement.MVC.Models
{
    public class Brand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandID { get; set; }
        [Required]
        [Display(Name =("Nom"))]
        public string BrandNom { get; set; }

        public ICollection<Produit> Produits { get; set; }

        public ICollection<Consent> Consents { get; set; }
    }
}
