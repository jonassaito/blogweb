using BlogWeb.DAO;
using BlogWeb.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BlogWeb.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        
        
        UsuarioDAO usuarioDAO;

        public LoginController(UsuarioDAO usuarioDAO)
        {
            this.usuarioDAO = usuarioDAO;

            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection(
                    "blog", "Usuario", "Id", "Login", true);
            }
        }



        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Autentica(string login, string senha)
        {
            Usuario usuario = usuarioDAO.Busca(login, senha);

            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(login, senha))
                {
                    Session["usuario"] = usuario;
                    string returnUrl = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ReturnUrl"];
                    

                    if (returnUrl == "/" || returnUrl == null)
                    {
                        return RedirectToAction("Index", "Postagem");
                    }
                    else
                    {
                        return Redirect("~" + returnUrl);
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("login.Invalido", "Login ou senha incorretos");
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            //Session.Abandon();
            WebSecurity.Logout();
            return RedirectToAction("Index");
        }
    }
}