using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Mapping;
using BlogWeb.Models;

namespace BlogWeb.Mapeamentos
{
    public class TagMapping : ClassMap<Tag>
    {
        public TagMapping()
        {
            Id(t => t.Id).GeneratedBy.Identity();
            Map(t=>t.Nome);
        }
    }
}