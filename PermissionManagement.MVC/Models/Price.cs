using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Models
{
        public enum Context
        {
            Production, Achat, Vente
        }
        public enum Currency
        {
            Dinar, Dollar, Euro
        }
        public class Price
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int PriceID { get; set; }
            [Required]
            public string PriceNom { get; set; }
            [Required]
            public Context? Context { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime DateDebut { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime DateFin { get; set; }
            [Required]
            public Currency? Currency { get; set; }
            public decimal Unit { get; set; }
        }
}
