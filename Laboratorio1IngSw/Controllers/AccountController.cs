using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Laboratorio1IngSw.Models;
using Laboratorio1IngSw.Models.ViewModel;
using Laboratorio1IngSw.Models.EntityManager;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Laboratorio1IngSw.Controllers
{
    public class AccountController : Controller
    {

        public AccountController()
        {
        }
        [AllowAnonymous]
        public ActionResult LogIn()
        {
           return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(UsuarioLoginView ulv, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserManager um = new UserManager();
                byte[] pwd = um.ObtenerContraseñaUsuario(ulv.Nombre);  

                if (pwd==null)
                {
                    ModelState.AddModelError("", "Datos indentificativos incorrectos.");  
                }
                else
                {
                    byte[] pwdenc;
                    SHA512 shaM = new SHA512Managed();
                    pwdenc = shaM.ComputeHash(Encoding.Unicode.GetBytes(ulv.Password)); 
                    if (Convert.ToBase64String(pwd).Equals(Convert.ToBase64String(pwdenc)))
                    {
                        FormsAuthentication.SetAuthCookie(ulv.Nombre, false);
                        return RedirectToAction("MenuAdministracion", "MenuAdministracion");
                    }
                    else { ModelState.AddModelError("", "Datos identificativos incorrectos."); }
                }
            }
            return View(ulv);
        }
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("LogIn");
        }

    }
}