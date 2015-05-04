using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BlogWeb.Models;
using BlogWeb.ViewModels;

namespace BlogWeb.App_Start
{
    public class AutoMapperConfig
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.CreateMap<Postagem, PostagemModel>();

            Mapper.CreateMap<PostagemModel,Postagem>()
                .ForMember(p => p.Autor, opcoes => {
                    opcoes.MapFrom(m => new Usuario(){Id = (int)m.AutorId});
                    opcoes.Condition(m => m.AutorId > 0);
                });

            
            Mapper.CreateMap<int, Tag>().ConvertUsing(id => new Tag() { Id = id });

            Mapper.CreateMap<Tag, int>().ConvertUsing(tag => tag.Id);
        }
    }
}