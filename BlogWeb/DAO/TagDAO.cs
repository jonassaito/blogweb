using BlogWeb.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogWeb.DAO
{
    public class TagDAO
    { 
        private ISession session;

        public TagDAO(ISession session)
        {
            this.session = session;
        }

        public IList<Tag> Lista()
        {
            string hql = "select t from Tag t order by t.Nome asc";
            IQuery query = session.CreateQuery(hql);
            return query.List<Tag>();
        }
        public IList<Tag> ListaPorPostagem(int id)
        {
            //string hql = "select pt from Postagem_Tags pt left join pt where pt.PostagemId = :id";
            string hql = "select p.Tags from Postagem p inner join fetch p.Tags t where p.Id =: id ";
            IQuery query = session.CreateQuery(hql);
            query.SetParameter("id", id);
            return query.List<Tag>();
        }
        public IList<KeyValuePair<Tag, int>> ListaTodosPorPostagem(int id)
        {
            

            IList<Tag> tagsPorPostagem = this.ListaPorPostagem(id);

            IList<Tag> tagsExcept = this.Lista().Except(this.ListaPorPostagem(id)).ToList();

            Dictionary<Tag, int> tagsDic = new Dictionary<Tag, int>(); ;
            foreach (var item in tagsPorPostagem)
            {
                Tag tag = new Tag();
                tag.Id = item.Id;
                tag.Nome = item.Nome;
                tagsDic.Add(tag, 1);
            }
            foreach (var item in tagsExcept)
            {
                Tag tag = new Tag();
                tag.Id = item.Id;
                tag.Nome = item.Nome;
                tagsDic.Add(tag, 0);
            }
            List<KeyValuePair<Tag, int>> tagsList = tagsDic.ToList();

            tagsList.Sort((firstPair, nextPair) =>
            {
                return firstPair.Key.Nome.CompareTo(nextPair.Key.Nome);
            }
            );
            return tagsList;
        }
    }
}