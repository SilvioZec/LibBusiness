using LibBusiness.Business.Core;
using LibBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibBusiness.Presentation.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for AdicionarLivroModal.xaml
    /// </summary>
    public partial class AdicionarLivroModal : Window
    {
        private bool sucesso;

        public bool Sucesso
        {
            get { return sucesso; }
            set { sucesso = value; }
        }

        private Livro livro = new Livro();

        public Livro Livro
        {
            get { return livro; }
            set { livro = value; }
        }

        private List<Categoria> selectedCategorias = new List<Categoria>();

        public List<Categoria> SelectedCategorias
        {
            get { return selectedCategorias; }
            set { selectedCategorias = value; }
        }


        GerenciadorCategorias gerenciadorCategorias = new GerenciadorCategorias();
        public List<Categoria> ListCategorias { get; set; }


        public AdicionarLivroModal()
        {
            InitializeComponent();
            sucesso = false;
            ListCategorias = gerenciadorCategorias.getCategorias();
            DataContext = this;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtISBN.Text) && !string.IsNullOrEmpty(txtAutor.Text) && !string.IsNullOrEmpty(txtTitulo.Text) && !string.IsNullOrEmpty(txtQuantidade.Text) && int.TryParse(txtQuantidade.Text, out _))
            {
                if (listCategorias.SelectedItems.Count > 1)
                {
                    livro.ISBN = txtISBN.Text;
                    livro.Autor = txtAutor.Text;
                    livro.Titulo = txtTitulo.Text;
                    livro.Quantidade = txtQuantidade.Text;

                    foreach (var item in listCategorias.SelectedItems)
                    {
                        if (item is Categoria categoria)
                        {
                            selectedCategorias.Add(categoria);
                        }
                    }

                    this.sucesso = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ao menos uma categoria precisa ser selecionada");
                }
            }
            else
            {
                MessageBox.Show("Preencha ISBN, Titulo, Autor e Quantidade (em numeros apenas) por favor");
            }
        }
    }
}
