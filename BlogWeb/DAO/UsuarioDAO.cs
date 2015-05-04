using BlogWeb.Infra;
using BlogWeb.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogWeb.DAO
{
    public class UsuarioDAO
    {
        private ISession session;

        public UsuarioDAO(ISession session)
        {
            this.session = session;
        }

        public void Adiciona(Usuario u)
        {

            ITransaction tx = session.BeginTransaction();
            session.Save(u);
            tx.Commit();

        }

        public IList<Usuario> Lista()
        {

            string hql = "select u from Usuario u";
            IQuery query = session.CreateQuery(hql);
            return query.List<Usuario>();

        }

        public void Atualiza(Usuario u)
        {

            ITransaction tx = session.BeginTransaction();
            session.Merge(u);
            tx.Commit();

        }

        public Usuario BuscaPorId(int id)
        {

            return session.Get<Usuario>(id);

        }

        public Usuario Busca(string login, string senha)
        {
            string hql = "select u from Usuario u where u.Login = :login and u.Password = :senha";
            IQuery query = session.CreateQuery(hql);
            query.SetParameter("login", login);
            query.SetParameter("senha", senha);
            return query.UniqueResult<Usuario>();

        }
    }
}