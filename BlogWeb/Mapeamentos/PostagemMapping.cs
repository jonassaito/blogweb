using BlogWeb.Models;
using FluentNHibernate.Mapping;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.Mapeamento
{
    public class PostagemMapping : ClassMap<Postagem>
    {
        public PostagemMapping()
        {
            Id(postagem => postagem.Id).GeneratedBy.Identity();
            Map(postagem => postagem.Titulo);
            Map(postagem => postagem.Conteudo);
            Map(postagem => postagem.DataPublicacao);
            Map(postagem => postagem.Publicado);
            References(postagem => postagem.Autor, "AutorId").Not.LazyLoad();
            HasManyToMany(postagem => postagem.Tags)
                .Table("Postagem_Tags")
                .ParentKeyColumn("PostagemId")
                .ChildKeyColumn("TagId").Not.LazyLoad();
        }
    }
}
