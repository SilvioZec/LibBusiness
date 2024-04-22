using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBusiness.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool Devolvido { get; set; } = false;
        //foreign keys and relationships
        
        [ForeignKey("Leitor")]
        public string LeitorId { get; set; }
        public virtual Leitor? Leitor { get; set; }

        public int LivroId { get; set; }
        public virtual Livro? Livro { get; set; }
       
    }
}
