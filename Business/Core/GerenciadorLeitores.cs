using LibBusiness.Models;
using LibBusiness.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibBusiness.Business.Core
{
    public class GerenciadorLeitores
    {
        private DBContext dbp = new DBContext();

        public void sincronizarDtgBd(DataGrid dtg)
        {
            var leitoresDatagrid = dtg.ItemsSource as List<Leitor>;

            // Obter todas as categorias da base de dados
            var leitoresBanco = getLeitores();

            // Inserir novas categorias na base de dados
            foreach (var leitor in leitoresDatagrid.Where(l => !leitoresBanco.Any(cb => cb.Id == l.Id)))
            {
                dbp.Leitores.Add(leitor);
            }

            // Excluir categorias que estão na base de dados e não estão no DataGrid
            foreach (var leitor in leitoresBanco.Where(lb => !leitoresDatagrid.Any(l => l.Id == lb.Id)))
            {
                dbp.Leitores.Remove(leitor);
            }


            // Atualizar as categorias que estão tanto no DataGrid quanto na base de dados
            foreach (var leitor in leitoresDatagrid.Where(l => leitoresBanco.Any(lb => lb.Id == l.Id)))
            {
                dbp.Update(leitor);
            }
            dbp.SaveChanges();
        }
        public void removeLeitor(string IDleitor)
        {
            var leitorToRemove = dbp.Leitores.FirstOrDefault(l => l.Id == IDleitor);
            if (leitorToRemove != null)
            {
                dbp.Leitores.Remove(leitorToRemove);
                dbp.SaveChanges();
            }
        }

        public List<Leitor> getLeitores()
        {
            return dbp.Leitores.ToList();
        }


        public List<Leitor> filtrarLeitores(string campo, string pesquisa)
        {
            if (campo == "Nome")
            {
                return dbp.Leitores.ToList().Where(l => l.Nome.IndexOf(pesquisa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            else if (campo == "Id")
            {
                return dbp.Leitores.ToList().Where(l => l.Id.IndexOf(pesquisa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            else
            {
                throw new Exception("Campo invalido");
            }

        }
    }
}
