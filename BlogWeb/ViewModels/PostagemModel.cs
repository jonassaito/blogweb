using BlogWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogWeb.ViewModels
{
    public class PostagemModel
    {
        public int Id { get; set; }

        [StringLengthAttribute(20, ErrorMessage = "Máximo 20 caracteres")]
        [MinLength(3, ErrorMessage = "Mínimo 3 caracteres.")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Conteudo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime? DataPublicacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public bool Publicado { get; set; }

        public int? AutorId { get; set; }

        public IList<int> Tags { get; set; }

        //public PostagemModel() { }
        //public PostagemModel(Postagem postagem)
        //{
        //    this.Id = postagem.Id;
        //    this.Titulo = postagem.Titulo;
        //    this.Conteudo = postagem.Conteudo;
        //    this.Publicado = postagem.Publicado;
        //    this.DataPublicacao = postagem.DataPublicacao;
        //    if (postagem.Autor != null)
        //    {
        //        this.AutorId = postagem.Autor.Id;
        //    }
        //}

        //public Postagem CriaPost()
        //{
        //    Postagem postagem = new Postagem()
        //    {
        //        Id = this.Id,
        //        Titulo = this.Titulo,
        //        Conteudo = this.Conteudo,
        //        Publicado = this.Publicado,
        //        DataPublicacao = this.DataPublicacao
        //    };
        //    if (this.AutorId != null)
        //    {
        //        Usuario autor = new Usuario()
        //        {
        //            Id = (int)this.AutorId
        //        };
        //        postagem.Autor = autor;
        //    }
        //    return postagem;
        //}

    }
}