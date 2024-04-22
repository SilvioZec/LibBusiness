using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBusiness.Models
{
    public class Leitor
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        //relationships
        public virtual ICollection<Emprestimo>? Emprestimos { get; set; }
        public override string ToString()
        {
            return Nome;
        }
    }
}
