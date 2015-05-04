using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogWeb.Models
{
    public class Usuario
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string Nome { get; set; }
        [Required]
        public virtual string Login { get; set; }
        [Required]
        public virtual string Password { get; set; }
        [Required]
        [EmailAddress(ErrorMessage="Email Invalido.")]
        public virtual string Email { get; set; }
    }
}