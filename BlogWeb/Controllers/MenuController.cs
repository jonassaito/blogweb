using BlogWeb.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWeb.Controllers
{
    [AllowAnonymous]
    public class MenuController : Controller
    {
        private PostagemDAO postagemDAO;
        private UsuarioDAO usuarioDAO;
        public MenuController(PostagemDAO postagemDAO,UsuarioDAO usuarioDAO)
        {
            this.postagemDAO = postagemDAO;
            this.usuarioDAO = usuarioDAO;
        }
        // GET: Menu
        public ActionResult Index()
        {
            ViewBag.Autores = usuarioDAO.Lista();
            ViewBag.Estatisticas = postagemDAO.PublicacoesPorMes();
            return PartialView();
        }
    }
}