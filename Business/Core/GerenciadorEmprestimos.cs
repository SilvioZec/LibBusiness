using LibBusiness.Models;
using LibBusiness.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBusiness.Business.Core
{
    public class GerenciadorEmprestimos
    {
        private DBContext dbp = new DBContext();

        public List<Emprestimo> getEmprestimos()
        {
            return dbp.Emprestimos.Include(e=> e.Leitor).Include(e => e.Livro).ToList();
        }

        public List<Emprestimo> getEmprestimosNaoDevolvidos()
        {
            return dbp.Emprestimos.Include(e => e.Leitor).Include(e => e.Livro).Where(e => e.Devolvido == false).ToList();
        }

        public void addEmprestimo(Emprestimo emprestimo)
        {
            Livro livro = dbp.Livros.FirstOrDefault(l => l.Id == emprestimo.LivroId);
            if (Convert.ToInt32(livro.Quantidade) > 0)
            {
                livro.Quantidade = (Convert.ToInt32(livro.Quantidade) - 1).ToString();
                dbp.Emprestimos.Add(emprestimo);
                dbp.SaveChanges();
            }
            else
            {
                throw new Exception("Quantidade de livros insuficiente para emprestimo");
            }
        }

        public void deleteEmprestimo(int id) 
        {
            Emprestimo emprestimo = dbp.Emprestimos.FirstOrDefault(e => e.Id == id);
            Livro livro = dbp.Livros.FirstOrDefault(l=> l.Id == emprestimo.LivroId);
            livro.Quantidade = (Convert.ToInt32(livro.Quantidade) + 1).ToString();
            dbp.Livros.Update(livro);
            dbp.Emprestimos.Remove(emprestimo);
            dbp.SaveChanges();
            
        }

        public void devolverLivro(int emprestimoId)
        {
            Emprestimo emprestimo = dbp.Emprestimos.FirstOrDefault(e => e.Id == emprestimoId);
            Livro livro = dbp.Livros.FirstOrDefault(l => l.Id == emprestimo.LivroId);

            emprestimo.Devolvido = true;
            livro.Quantidade = (Convert.ToInt32(livro.Quantidade) + 1).ToString();

            dbp.Emprestimos.Update(emprestimo);
            dbp.Livros.Update(livro);
            dbp.SaveChanges();
        }

    }
}
