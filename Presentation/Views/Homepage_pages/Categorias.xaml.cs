using LibBusiness.Business.Core;
using LibBusiness.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibBusiness.Presentation.Views.Homepage_pages
{
    /// <summary>
    /// Interaction logic for Categorias.xaml
    /// </summary>
    public partial class Categorias : Page
    {
        private GerenciadorCategorias gerenciadorCategorias = new GerenciadorCategorias();

            
        private void criarDatagrid()
        {
            dtgCategorias.ItemsSource = gerenciadorCategorias.getCategorias();
            dtgCategorias.CanUserAddRows = true;

            // Ocultar coluna de LivroCategorias e alterar o nome de exibição das colunas Id e NomeCategoria
            dtgCategorias.Loaded += (sender, e) =>
            {
                var dataGrid = sender as DataGrid;
                if (dataGrid != null)
                {
                    var colNumeroCategoria = dataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "Id");
                    if (colNumeroCategoria != null)
                    {
                        colNumeroCategoria.Header = "Número da Categoria";
                    }

                    var colDesignacao = dataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "NomeCategoria");
                    if (colDesignacao != null)
                    {
                        colDesignacao.Header = "Designação";
                    }

                    var colLivroCategorias = dataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "LivroCategorias");
                    if (colLivroCategorias != null)
                    {
                        colLivroCategorias.Visibility = Visibility.Collapsed;
                    }
                }
            };
        }
        public Categorias()
        {
            InitializeComponent();
            
            criarDatagrid();
            
        }

        private void btnSyncGrid_Click(object sender, RoutedEventArgs e)
        {
            gerenciadorCategorias.sincronizarDtgBd(dtgCategorias);
        }

        private void btnCancelGrid_Click(object sender, RoutedEventArgs e)
        {
            criarDatagrid();
            var colLivroCategorias = dtgCategorias.Columns.FirstOrDefault(c => c.Header.ToString() == "LivroCategorias");
            if (colLivroCategorias != null)
            {
                colLivroCategorias.Visibility = Visibility.Collapsed;
            }
        }

        private void btnExcluirCat_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCategorias.SelectedItem != null && dtgCategorias.SelectedItem is Categoria)
            {
                Categoria categoria = (Categoria)dtgCategorias.SelectedItem; // Obter a categoria selecionada

                var result = MessageBox.Show($"Deseja mesmo excluir a categoria com o ID {categoria.Id}?", "Confirmação de exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Aqui você pode implementar a lógica de exclusão da categoria
                    gerenciadorCategorias.removeCategoria(categoria.Id);

                    // Atualize o DataGrid após a exclusão, se necessário
                    criarDatagrid();
                    var colLivroCategorias = dtgCategorias.Columns.FirstOrDefault(c => c.Header.ToString() == "LivroCategorias");
                    if (colLivroCategorias != null)
                    {
                        colLivroCategorias.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma categoria para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
