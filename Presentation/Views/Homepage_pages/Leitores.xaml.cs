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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibBusiness.Presentation.Views.Homepage_pages
{
    /// <summary>
    /// Interaction logic for Leitores.xaml
    /// </summary>
    public partial class Leitores : Page
    {
        private GerenciadorLeitores gerenciadorLeitores = new GerenciadorLeitores();
        private void criarDatagrid()
        {
            dtgLeitores.ItemsSource = gerenciadorLeitores.getLeitores();
            dtgLeitores.CanUserAddRows = true;

            // Ocultar coluna de LivroCategorias e alterar o nome de exibição das colunas Id e NomeCategoria
            dtgLeitores.Loaded += (sender, e) =>
            {
                var dataGrid = sender as DataGrid;
                if (dataGrid != null)
                {
                    var colIdLeitor = dataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "Id");
                    if (colIdLeitor != null)
                    {
                        colIdLeitor.Header = "ID do Leitor";
                    }


                    var colEmprestimos = dataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "Emprestimos");
                    if (colEmprestimos != null)
                    {
                        colEmprestimos.Visibility = Visibility.Collapsed;
                    }
                }
            };
        }
        public Leitores()
        {
            InitializeComponent();
            criarDatagrid();
        }

        private void btnExcluirLeitor_Click(object sender, RoutedEventArgs e)
        {
            if (dtgLeitores.SelectedItem != null && dtgLeitores.SelectedItem is Leitor)
            {
                Leitor leitor = (Leitor)dtgLeitores.SelectedItem; // Obter leitor selecionado

                var result = MessageBox.Show($"Deseja mesmo excluir o leitor com o ID {leitor.Id}?", "Confirmação de exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Aqui você pode implementar a lógica de exclusão da categoria
                    gerenciadorLeitores.removeLeitor(leitor.Id);

                    // Atualize o DataGrid após a exclusão, se necessário
                    criarDatagrid();
                    
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma categoria para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancelGridL_Click(object sender, RoutedEventArgs e)
        {
            criarDatagrid();
            var colLivroCategorias = dtgLeitores.Columns.FirstOrDefault(c => c.Header.ToString() == "Emprestimos");
            if (colLivroCategorias != null)
            {
                colLivroCategorias.Visibility = Visibility.Collapsed;
            }
        }

        private void btnSyncGridL_Click(object sender, RoutedEventArgs e)
        {
            gerenciadorLeitores.sincronizarDtgBd(dtgLeitores);
        }
    }
}
