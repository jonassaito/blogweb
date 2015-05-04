using BlogWeb.Infra;
using BlogWeb.Models;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb
{
    public class PostagemDAO
    {
        private ISession session;

        public PostagemDAO(ISession session)
        {
            this.session = session;
        }

        public int Adiciona(Postagem postagem)
        {

            ITransaction tx = session.BeginTransaction();
            int retorno = (int)session.Save(postagem);
            tx.Commit();
            return retorno;



        }

        public Postagem BuscaPorId(int Id)
        {

            return session.Get<Postagem>(Id);

        }
        public void Remove(Postagem postagem)
        {

            ITransaction tx = session.BeginTransaction();
            session.Delete(postagem);

        }

        public Postagem Atualiza(Postagem post)
        {

            ITransaction tx = session.BeginTransaction();
            session.Merge(post);
            tx.Commit();
            return post;

        }
        public IList<Postagem> Lista()
        {


            string hql = "select p from Postagem p order by p.Id desc";
            //IQuery query = session.CreateQuery(hql).SetMaxResults(4);
            IQuery query = session.CreateQuery(hql);
            return query.List<Postagem>();


        }
        public IList<Postagem> ListaPublicados()
        {


            string hql = "select p from Postagem p where p.Publicado = true order by p.DataPublicacao desc";
            //IQuery query = session.CreateQuery(hql).SetMaxResults(4);
            IQuery query = session.CreateQuery(hql);
            return query.List<Postagem>();


        }
        public IList<Postagem> ListaPublicadosDoAutor(string nome)
        {


            string hql = "select p from Postagem p where p.Publicado = true and p.Autor.Nome = :nome order by p.DataPublicacao desc";
            //IQuery query = session.CreateQuery(hql).SetMaxResults(4);
            IQuery query = session.CreateQuery(hql);
            query.SetParameter("nome", nome);
            return query.List<Postagem>();


        }
        public IList<PostagensPorMes> PublicacoesPorMes()
        {


            string hql = "select month(p.DataPublicacao) as Mes, year(p.DataPublicacao) as Ano,  count(p) as Quantidade from Postagem p where p.Publicado = true group by month(p.DataPublicacao), year(p.DataPublicacao) ";
            IQuery query = session.CreateQuery(hql);
            query.SetResultTransformer(Transformers.AliasToBean<PostagensPorMes>());
            return query.List<PostagensPorMes>();


        }


        public IList<Postagem> ListaPublicadosPorMesAno(int mes, int ano)
        {


            string hql = "select p from Postagem p where p.Publicado = true and month(p.DataPublicacao) = :mes and year(p.DataPublicacao) = :ano order by p.DataPublicacao desc";
            //IQuery query = session.CreateQuery(hql).SetMaxResults(4);
            IQuery query = session.CreateQuery(hql);
            query.SetParameter("mes", mes);
            query.SetParameter("ano", ano);
            return query.List<Postagem>();


        }
    }
}
