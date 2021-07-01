using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
    public enum A_Statut
    {
        Récu, Non_Récu
    }
    public class Allocation 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AllocationID { get; set; }
        [Display(Name = ("Type"))]

        public string A_Type { get; set; }
        [Display(Name = ("Compte"))]

        public int? CompteID { get; set; }
        [Display(Name = ("Contact"))]

        public int? ContactID { get; set; }
        [Display(Name = ("Produit"))]

        public int ProduitID { get; set; }
        [Display(Name = ("Created On"))]

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime A_CreatedOn { get; set; } = DateTime.Now;
        [Display(Name = ("For (date)"))]

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime A_To { get; set; }
        public int Quantity { get; set; }
        [Display(Name = ("Statut"))]

        public A_Statut A_Statut { get; set; } = (A_Statut)1;

        public Compte Compte { get; set; }
        public Produit Produit { get; set; }
        public Contact Contact { get; set; }

    }
}
