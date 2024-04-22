using LibBusiness.Business.Core;
using LibBusiness.Models;
using LibBusiness.Presentation.Resources.CustomControls;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibBusiness.Presentation.Views.Homepage_pages
{
    /// <summary>
    /// Interaction logic for Livros.xaml
    /// </summary>
    public partial class Livros : Page
    {
        private GerenciadorLivros gerenciadorLivros = new GerenciadorLivros();
        public ObservableCollection<string> ItemsList { get; set; }
        public Livros()
        {
            InitializeComponent();
            dtgLivros.ItemsSource = gerenciadorLivros.getLivros();
            ItemsList = new ObservableCollection<string>
            {
                "Titulo",
                "ISBN",
                "Autor",
                "Categoria"
            };
            cbxSearch.SelectedItem = ItemsList.FirstOrDefault();
            //dtgLivros.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            DataContext = this;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgLivros.ItemsSource = gerenciadorLivros.filtrarLivros(cbxSearch.SelectedItem.ToString(), txtSearch.Text);
        }

        private void cbxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtgLivros.ItemsSource = gerenciadorLivros.filtrarLivros(cbxSearch.SelectedItem.ToString(), txtSearch.Text);
        }

        private void btnAddLivro_Click(object sender, RoutedEventArgs e)
        {
            AdicionarLivroModal addLivroModal = new AdicionarLivroModal();
            addLivroModal.ShowDialog();
            if (addLivroModal.Sucesso)
            {
                gerenciadorLivros.addLivro(addLivroModal.Livro, addLivroModal.SelectedCategorias);
                dtgLivros.ItemsSource = gerenciadorLivros.getLivros();
            }
        }

        private void btnRemoveLivro_Click(object sender, RoutedEventArgs e)
        {
            if (dtgLivros.SelectedItem is ViewLivroModel livro)
            {
                gerenciadorLivros.removeLivro(livro.Id);
                dtgLivros.ItemsSource = gerenciadorLivros.filtrarLivros(cbxSearch.SelectedItem.ToString(), txtSearch.Text);
                
            }
            else
            {
                MessageBox.Show("Não foi possivel remover o livro");
            }
        }

        private void imgExcel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ExcelHelpModal excelHelp = new ExcelHelpModal();
            excelHelp.ShowDialog();
            if (excelHelp.Prosseguir)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Ficheiro em Excel | *.xlsx";
                bool? escolhido = openFileDialog.ShowDialog();
                if (escolhido == true)
                {
                    string caminho = openFileDialog.FileName;
                    gerenciadorLivros.addExcel(caminho);
                    dtgLivros.ItemsSource = gerenciadorLivros.filtrarLivros(cbxSearch.SelectedItem.ToString(), txtSearch.Text);
                }
            }
        }

        private void btnAtualizar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dtgLivros.ItemsSource = gerenciadorLivros.getLivros();
        }
    }
}
