using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBusiness.Models
{
    public class LivroCategoria
    {
        public int Id { get; set; }

        //foreign keys
        public int LivroId { get; set; }
        public int CategoriaId { get; set; }

        //relationships

        public virtual Livro? Livro { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}
