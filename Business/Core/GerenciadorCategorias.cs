using LibBusiness.Models;
using LibBusiness.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibBusiness.Business.Core
{
    public class GerenciadorCategorias
    {
        private DBContext dbp = new DBContext();
        public void sincronizarDtgBd(DataGrid dtg)
        {
            var categoriasDatagrid = dtg.ItemsSource as List<Categoria>;

            // Obter todas as categorias da base de dados
            var categoriasBanco = getCategorias();

            // Inserir novas categorias na base de dados
            foreach (var categoria in categoriasDatagrid.Where(c => !categoriasBanco.Any(cb => cb.Id == c.Id)))
            {
                dbp.Categorias.Add(categoria);
            }

            // Excluir categorias que estão na base de dados e não estão no DataGrid
            foreach (var categoria in categoriasBanco.Where(cb => !categoriasDatagrid.Any(c => c.Id == cb.Id)))
            {
                dbp.Categorias.Remove(categoria);
            }


            // Atualizar as categorias que estão tanto no DataGrid quanto na base de dados
            foreach (var categoria in categoriasDatagrid.Where(c => categoriasBanco.Any(cb => cb.Id == c.Id)))
            {
                dbp.Update(categoria);
            }
            dbp.SaveChanges();
        }
        public void removeCategoria(int IDcategoria)
        {
            var categoriaToRemove = dbp.Categorias.FirstOrDefault(c=> c.Id == IDcategoria);
            if (categoriaToRemove != null)
            {
                dbp.Categorias.Remove(categoriaToRemove);
                dbp.SaveChanges();
            }
        }

        public List<Categoria> getCategorias()
        {
            return dbp.Categorias.ToList();
        }

        
    }
}
