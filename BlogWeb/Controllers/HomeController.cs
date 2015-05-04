using BlogWeb;
using BlogWeb.Infra;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWeb.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private PostagemDAO postagemDAO;

        public HomeController(PostagemDAO postagemDAO)
        {
            this.postagemDAO = postagemDAO;
        }
        // GET: Home
        public ActionResult Index()
        {

            return View(postagemDAO.ListaPublicados());
        }

    }
}