using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBusiness.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Quantidade { get; set; }

        //relationships
        public virtual ICollection<LivroCategoria>? LivroCategorias { get; set; }
        public virtual ICollection<Emprestimo>? Emprestimos { get; set; }

        public override string ToString()
        {
            return Titulo;
        }

    }
}
