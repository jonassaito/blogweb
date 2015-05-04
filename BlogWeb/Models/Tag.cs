using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogWeb.Models
{
    public class Tag
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
    }
}