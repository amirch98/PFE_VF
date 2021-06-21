using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Controllers
{
    public class ChangeLog
    {
        public static string GetUserLog(string user, string log, string Objet, int id, string name)
        {
            string now = DateTime.Now.ToString();
            return user + " a " + log + " " + Objet + " ID: " + id + " avec le nom " + name + " (" + now +")";
        }
    }
}
