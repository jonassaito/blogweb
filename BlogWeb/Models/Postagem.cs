using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.Models
{
    //POCO Plain Old CLR Object
    public class Postagem
    {
        public virtual int Id { get; set; }

        [StringLengthAttribute(20,ErrorMessage="Máximo 20 caracteres")]
        [MinLength(3,ErrorMessage="Mínimo 3 caracteres.")]
        [Required(ErrorMessage="Campo obrigatório.")]
        public virtual string Titulo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public virtual string Conteudo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public virtual DateTime? DataPublicacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public virtual bool Publicado { get; set; }

        public virtual Usuario Autor { get; set; }

        public virtual IList<Tag> Tags { get; set; }

    }
}
