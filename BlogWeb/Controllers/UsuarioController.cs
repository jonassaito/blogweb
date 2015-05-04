using BlogWeb.DAO;
using BlogWeb.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace BlogWeb.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : Controller
    {
        private UsuarioDAO usuarioDAO;

        public UsuarioController(UsuarioDAO usuarioDAO)
        {
            this.usuarioDAO = usuarioDAO;
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection(
                    "blog", "Usuario", "Id", "Login", true);
            }
        }
        // GET: Usuario
        public ActionResult Index()
        {
            IList<Usuario> usuarios = usuarioDAO.Lista();
            return View(usuarios);
        }
        [Route("novoUsuario")]
        public ActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Adiciona(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //usuarioDAO.Adiciona(usuario);
                    WebSecurity.CreateUserAndAccount(usuario.Login, usuario.Password,
                        new { Email = usuario.Email, Nome = usuario.Nome }, false);
                    return RedirectToAction("Index");

                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("usuario.Invalido", e.Message);
                    return View("Form");
                }
            }
            else
            {
                return View("Form", usuario);
            }
        }
        [HttpPost]
        public ActionResult Altera(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    WebSecurity.CreateAccount(usuario.Login, usuario.Password, false);
                }
                catch (MembershipCreateUserException e)
                {

                    ModelState.AddModelError("alteracao.Invalida", e.Message);
                    return View("Visualiza", usuario);
                }

                usuarioDAO.Atualiza(usuario);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Visualiza", usuario);
            }
        }
        public ActionResult Visualiza(int id)
        {
            Usuario u = usuarioDAO.BuscaPorId(id);
            return View(u);
        }
    }
}