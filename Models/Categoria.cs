using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBusiness.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string NomeCategoria { get; set; }

        //relationships
        public virtual ICollection<LivroCategoria>? LivroCategorias { get; set; }
    }
}
