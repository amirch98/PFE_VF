using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
    public enum Type
    {
        Brand, Lot
    }
    public enum Unite
    {
        Carton, Paquet
    }
    public enum Competitor
    {
        Oui, Non
    }
    public class Produit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProduitID { get; set; }
        [Required]
        [Display(Name = ("Name"))]
        public string ProduitNom { get; set; }
        public Type? Type { get; set; }
        [Display(Name = ("Brand"))]

        public int BrandID { get; set; }
        public Competitor? Competitor { get; set; }
        public Unite? Unite { get; set; }
        [Display(Name = ("Price"))]

        public int? PriceID { get; set; }
        [Display(Name = ("Description"))]
        public string PDescription { get; set; }
        [Display(Name = ("Theoretical Stock"))]

        public int Stock_Theorique { get; set; }
        [Display(Name = ("Physical Stock"))]

        public int Stock_Physique { get; set; }


        public Brand Brand { get; set; }
        public Price Price { get; set; }

        public ICollection<Allocation> Allocations { get; set; }
        public ICollection<Price> Prices { get; set; }

    }
}
