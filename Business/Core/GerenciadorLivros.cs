using LibBusiness.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System;
using System.IO;
using LibBusiness.Models;
using System.Threading.Tasks.Dataflow;
using System.Linq.Dynamic;
using LibBusiness.Presentation.Views.Homepage_pages;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls.Primitives;

namespace LibBusiness.Business.Core
{
    public class GerenciadorLivros
    {
        
        private DBContext dbp = new DBContext();
        public GerenciadorLivros()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        /// <summary>
        /// Adiciona os registros de uma planilha excel diretamente no banco de dados, dado que as colunas correspondem com um objeto tipo livro e as categorias estao separadas por virgula
        /// </summary>
        /// <param name="caminhoExcel"> caminho para o ficheiro excel</param>
        public void addExcel(string caminhoExcel)
        {
            using (var package = new ExcelPackage(new FileInfo(caminhoExcel)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assume que os dados estão na primeira planilha

                int rowStart = worksheet.Dimension.Start.Row;
                int rowEnd = worksheet.Dimension.End.Row;

                for (int row = rowStart + 1; row <= rowEnd; row++) // Inicia a partir da segunda linha, assumindo cabeçalho na primeira linha
                {
                    string isbn = worksheet.Cells[row, 1].Value?.ToString(); // Coluna 1 (ISBN)
                    string titulo = worksheet.Cells[row, 2].Value?.ToString(); // Coluna 2 (Título)
                    string autor = worksheet.Cells[row, 3].Value?.ToString(); // Coluna 3 (Autor)
                    string quantidade = worksheet.Cells[row, 4].Value?.ToString(); // Coluna 4 (Quantidade)
                    string categoriasString = worksheet.Cells[row, 5].Value?.ToString(); // Coluna 5 (Categorias)

                    // Verifica se algum dos valores essenciais está vazio
                    if (!string.IsNullOrEmpty(isbn) && !string.IsNullOrEmpty(titulo) && !string.IsNullOrEmpty(autor) && !string.IsNullOrEmpty(quantidade) && !string.IsNullOrEmpty(categoriasString))
                    {
                        Livro novoLivro = new Livro
                        {
                            ISBN = isbn,
                            Titulo = titulo,
                            Autor = autor,
                            Quantidade = quantidade
                        };

                        // Adiciona o novo livro ao contexto
                        dbp.Livros.Add(novoLivro);
                        dbp.SaveChanges();

                        // Obtém os números das categorias e os converte em uma lista de inteiros
                        List<string> nomesCategorias = categoriasString.Split(',').ToList();

                        // Adiciona as entradas de LivroCategoria para cada categoria do livro
                        foreach (string nomeCategoria in nomesCategorias)
                        {
                            Categoria categoria = dbp.Categorias.FirstOrDefault(c => c.NomeCategoria.Equals(nomeCategoria));
                            if (categoria != null)
                            {
                                LivroCategoria livroCategoria = new LivroCategoria
                                {
                                    LivroId = novoLivro.Id,
                                    CategoriaId = categoria.Id
                                };
                                dbp.LivroCategorias.Add(livroCategoria);

                            }
                            
                        }

                    }
                }

                dbp.SaveChanges(); // Salva as alterações na base de dados
            }
        }

        public List<ViewLivroModel> getLivros()
        {
            List<ViewLivroModel> listaCustomizada = new List<ViewLivroModel>();
            foreach (Livro livro in dbp.Livros)
            {
                //adicionar dados do livro
                ViewLivroModel itemCustomizado = new ViewLivroModel();
                itemCustomizado.Id = livro.Id;
                itemCustomizado.ISBN = livro.ISBN;
                itemCustomizado.Titulo = livro.Titulo;
                itemCustomizado.Autor = livro.Autor;
                itemCustomizado.Quantidade = livro.Quantidade;

                //criar string categorias
                StringBuilder sb = new StringBuilder();
                foreach (LivroCategoria item in dbp.LivroCategorias.Where(lc => lc.LivroId == livro.Id))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(dbp.Categorias.FirstOrDefault(c => c.Id == item.CategoriaId).NomeCategoria);
                }
                itemCustomizado.Categorias = sb.ToString();

                //adicionar a lista
                listaCustomizada.Add(itemCustomizado);
            }
            return listaCustomizada;
        }

        public List<ViewLivroModel> getLivros(List<Livro> livros)
        {
            List<ViewLivroModel> listaCustomizada = new List<ViewLivroModel>();
            foreach (Livro livro in livros)
            {
                //adicionar dados do livro
                ViewLivroModel itemCustomizado = new ViewLivroModel();
                itemCustomizado.Id = livro.Id;
                itemCustomizado.ISBN = livro.ISBN;
                itemCustomizado.Titulo = livro.Titulo;
                itemCustomizado.Autor = livro.Autor;
                itemCustomizado.Quantidade = livro.Quantidade;

                //criar string categorias
                StringBuilder sb = new StringBuilder();
                foreach (LivroCategoria item in dbp.LivroCategorias.Where(lc => lc.LivroId == livro.Id))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(dbp.Categorias.FirstOrDefault(c => c.Id == item.CategoriaId).NomeCategoria);
                }
                itemCustomizado.Categorias = sb.ToString();

                //adicionar a lista
                listaCustomizada.Add(itemCustomizado);
            }
            return listaCustomizada;
        }

        public List<ViewLivroModel> filtrarLivros(string campo, string pesquisa)
        {

            List<int> livroIds = new List<int>();

            // Consultar os livros com base no critério de pesquisa
            List<Livro> livrosFiltrados = new List<Livro>();
            if (campo == "Categoria")
            {
                //buscar id das categorias
                List<Categoria> categorias = dbp.Categorias.ToList().Where(c => c.NomeCategoria.IndexOf(pesquisa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                List<Livro> livrosBanco = dbp.Livros.Include(l => l.LivroCategorias).ToList();

                //Buscar ids dos livros em livrocategoria que contem ids das categorias
                List<int> livrosId = new List<int>();
                foreach (var categoria in categorias)
                {
                    var livrinId = livrosBanco
                        .Where(l => l.LivroCategorias.Any(lc => lc.CategoriaId == categoria.Id))
                        .Select(l => l.Id)
                        .ToList();

                    livrosId.AddRange(livrinId);
                }

                // Buscar os livros com base nos ids obtidos
                livrosFiltrados = dbp.Livros
                    .Where(l => livrosId.Contains(l.Id))
                    .ToList();

            }
            else if (campo == "Titulo")
            {
                livrosFiltrados = dbp.Livros.ToList().Where(l => l.Titulo.IndexOf(pesquisa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            else if(campo == "ISBN")
            {
                livrosFiltrados = dbp.Livros.ToList().Where(l => l.ISBN.IndexOf(pesquisa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            else if (campo == "Autor")
            {
                livrosFiltrados = dbp.Livros.ToList().Where(l => l.Autor.IndexOf(pesquisa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            return getLivros(livrosFiltrados);

        }

        public void addLivro(Livro livro, List<Categoria> categorias)
        {
            dbp.Livros.Add(livro);
            dbp.SaveChanges();
            foreach (Categoria item in categorias)
            {
                LivroCategoria livroCategoria = new LivroCategoria();
                livroCategoria.LivroId = livro.Id;
                livroCategoria.CategoriaId = item.Id;
                dbp.LivroCategorias.Add(livroCategoria);
            }
            dbp.SaveChanges();
        }
        public void removeLivro(int  id)
        {
            var livroCategorias = dbp.LivroCategorias.Where(lc => lc.LivroId == id).ToList();
            foreach (LivroCategoria item in livroCategorias)
            {
                dbp.Remove(item);
            }
            dbp.Livros.Remove(dbp.Livros.FirstOrDefault(l => l.Id == id));
            dbp.SaveChanges();
        }
    }
}

