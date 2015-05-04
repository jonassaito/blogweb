using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWeb.Controllers
{
    public class BuscaController : Controller
    {
        private PostagemDAO postagemDAO;

        public BuscaController(PostagemDAO postagemDAO)
        {
            this.postagemDAO = postagemDAO;
        }
        // GET: Busca
        public ActionResult Index()
        {
            return View();
        }

        [Route("Busca/Autor/{nome}", Name = "BuscaPorAutor")]
        public ActionResult BuscaPorAutor(string nome)
        {

            return View(postagemDAO.ListaPublicadosDoAutor(nome));
        }

        [Route("Busca/Ano/{ano}/Mes/{mes}", Name = "BuscaPorMesAno")]
        public ActionResult BuscaPorMesAno(int mes, int ano)
        {
            return View(postagemDAO.ListaPublicadosPorMesAno(mes,ano));

        }
    }
}