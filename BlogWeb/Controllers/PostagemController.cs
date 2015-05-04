using AutoMapper;
using BlogWeb;
using BlogWeb.DAO;
using BlogWeb.Filters;
using BlogWeb.Infra;
using BlogWeb.Models;
using BlogWeb.ViewModels;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWeb.Controllers
{
    //[AutorizacaoFilter]
    public class PostagemController : Controller
    {
        private PostagemDAO postagemDAO;
        private UsuarioDAO usuarioDAO;
        private TagDAO tagDAO;

        public PostagemController(PostagemDAO postagemDAO, UsuarioDAO usuarioDAO, TagDAO tagDAO)
        {
            this.postagemDAO = postagemDAO;
            this.usuarioDAO = usuarioDAO;
            this.tagDAO = tagDAO;
        }

        // GET: Postagem
        //[Route("Postagem")]
        [Route("posts")]
        public ActionResult Index()
        {
            IList<Postagem> postagens = postagemDAO.Lista();
            //ViewBag.postagens = postagens;
            return View(postagens);
        }
        [Route("novoPost")]
        public ActionResult Form()
        {
            ViewBag.Usuarios = usuarioDAO.Lista();
            ViewBag.Tags = tagDAO.Lista();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adiciona(PostagemModel postagemViewModel)
        {
            if (postagemViewModel.Publicado && !postagemViewModel.DataPublicacao.HasValue)
            {
                ModelState.AddModelError("postagem.Invalido", "Posts publicados precisam de data.");
            }
            if (ModelState.IsValid)
            {
                //Postagem postagem = postagemViewModel.CriaPost();
                Postagem postagem = Mapper.Map<Postagem>(postagemViewModel);
                postagemDAO.Adiciona(postagem);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Usuarios = usuarioDAO.Lista();
                ViewBag.Tags = tagDAO.Lista();
                return View("Form", postagemViewModel);
            }

        }

        public ActionResult Remove(int id)
        {
            Postagem postagem = postagemDAO.BuscaPorId(id);
            postagemDAO.Remove(postagem);
            return RedirectToAction("Index");
        }


        //[Route("Postagem/{id}", Name = "VerPostagem")]
        [Route("posts/{id}", Name = "VerPost")]
        public ActionResult Visualiza(int id)
        {
            Postagem postagem = postagemDAO.BuscaPorId(id);
            //PostagemModel postagemModel = new PostagemModel(postagem);
            PostagemModel postagemModel = Mapper.Map<PostagemModel>(postagem);
            ViewBag.Usuarios = usuarioDAO.Lista();

            //IList<Tag> tags = tagDAO.Lista();

            //ViewBag.Tags = tags;

            //IList<Tag> tagsPorPostagem = tagDAO.ListaPorPostagem(id);
            //ViewBag.TagsPorPostagem = tagsPorPostagem;

            //IList<Tag> tagsExcept = tagDAO.Lista().Except(tagDAO.ListaPorPostagem(id)).ToList();

            //Dictionary<Tag, int> tagsDic = new Dictionary<Tag, int>(); ;
            //foreach (var item in tagsPorPostagem)
            //{
            //    Tag tag = new Tag();
            //    tag.Id = item.Id;
            //    tag.Nome = item.Nome;
            //    tagsDic.Add(tag, 1);
            //}
            //foreach (var item in tagsExcept)
            //{
            //    Tag tag = new Tag();
            //    tag.Id = item.Id;
            //    tag.Nome = item.Nome;
            //    tagsDic.Add(tag, 0);
            //}
            //List<KeyValuePair<Tag, int>> tagsList = tagsDic.ToList();

            //tagsList.Sort((firstPair, nextPair) =>
            //{
            //    return firstPair.Key.Nome.CompareTo(nextPair.Key.Nome);
            //}
            //);

            ViewBag.tagsFinal = tagDAO.ListaTodosPorPostagem(id);

           

            return View(postagemModel);
        }
        public ActionResult Altera(PostagemModel postagemViewModel)
        {
            if (ModelState.IsValid)
            {
                //Postagem postagem = postagemModel.CriaPost();
                Postagem postagem = Mapper.Map<Postagem>(postagemViewModel);
                postagemDAO.Atualiza(postagem);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Usuarios = usuarioDAO.Lista();
                ViewBag.Tags = tagDAO.Lista();
                return View("Visualiza", postagemViewModel);
            }
        }

        public ActionResult Publica(int id)
        {
            Postagem postagem = postagemDAO.BuscaPorId(id);
            postagem.Publicado = true;
            postagem.DataPublicacao = DateTime.Now;
            postagemDAO.Atualiza(postagem);
            //return new EmptyResult();
            return Json(postagem.Publicado, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PublicaNaoPublica(int id)
        {
            Postagem postagem = postagemDAO.BuscaPorId(id);
            if (postagem.Publicado)
            {
                postagem.Publicado = false;

            }
            else
            {
                postagem.Publicado = true;
            }
            postagem.DataPublicacao = DateTime.Now;
            postagemDAO.Atualiza(postagem);
            //return new EmptyResult();
            return Json(postagem.Publicado, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Estatisticas()
        //{

        //    return PartialView(postagemDAO.PublicacoesPorMes());
        //}
    }
}


