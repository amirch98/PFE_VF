using System;

namespace PermissionManagement.MVC.Models
{
    public class Consent 
    {
        public int ConsentID { get; set; }
        public string Title { get; set; }
        public int ContactID { get; set; }
        public int BrandID { get; set; }
        public DateTime C_CreatedOn { get; set; } = DateTime.Now;
        public string C_CreatedBy { get; set; }
        public string Statut { get; set; } = "En Cours";

        public Contact Contact { get; set; }
        public Brand Brand { get; set; }


    }
}
