using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TreeManager.Domain.Concrete;
using TreeManager.Domain.Entities;
using TreeManager.WebUI.Models;

namespace TreeManager.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(details.Name,
                    details.Password);
                if(user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password.");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, 
                    ident);

                    //jesli zalogujemy sie ze strony glownej, to adres bedzie pusty
                    if (returnUrl != null && returnUrl != "")
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Tree", "Tree");
                    }


                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(details);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Tree", "Tree");
        }

        public ActionResult Denied()
        {
            return View();
        }
    }
}
