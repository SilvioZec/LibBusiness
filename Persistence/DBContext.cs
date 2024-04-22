using Microsoft.EntityFrameworkCore;
using LibBusiness.Models;
using System.IO;

namespace LibBusiness.Persistence
{
    public class DBContext : DbContext
    {

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Leitor> Leitores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<LivroCategoria> LivroCategorias { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }

        public string path = Path.Combine(Directory.GetCurrentDirectory(), "Persistence", "Database", "libBusiness.db");
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={path}");
    }
}