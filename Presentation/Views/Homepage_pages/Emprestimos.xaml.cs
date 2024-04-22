using LibBusiness.Business.Core;
using LibBusiness.Models;
using LibBusiness.Presentation.Resources.CustomControls;
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
    /// Interaction logic for Emprestimos.xaml
    /// </summary>
    public partial class Emprestimos : Page
    {
        GerenciadorEmprestimos gerenciadorEmprestimos = new GerenciadorEmprestimos();
        List<Emprestimo> listaDeEmprestimos = new List<Emprestimo>();

        private void montaGrid ()
        {
            
            int atrasos = listaDeEmprestimos.Count(e => e.Devolvido == false && e.DataDevolucao < DateTime.Today);
            if (atrasos == 1)
            {
                txtEmprestimos.Text = $"Existe {atrasos} emprestimo em atraso :(";
                Color cor = (Color)ColorConverter.ConvertFromString("#c93434");
                Brush brush = new SolidColorBrush(cor);

                // Define a cor do texto do TextBox
                txtEmprestimos.Foreground = brush;
            }
            else if (atrasos > 1)
            {
                txtEmprestimos.Text = $"Existem {atrasos} emprestimos em atraso :(";
                Color cor = (Color)ColorConverter.ConvertFromString("#c93434");
                Brush brush = new SolidColorBrush(cor);

                // Define a cor do texto do TextBox
                txtEmprestimos.Foreground = brush;
            }
            else
            {
                txtEmprestimos.Text = $"Não há emprestimos em atraso :)";
                Color cor = (Color)ColorConverter.ConvertFromString("#3f3f3f");
                Brush brush = new SolidColorBrush(cor);

                // Define a cor do texto do TextBox
                txtEmprestimos.Foreground = brush;
            }
            listaDeEmprestimos = gerenciadorEmprestimos.getEmprestimosNaoDevolvidos();
            dtgEmprestimos.ItemsSource = listaDeEmprestimos;

        }
        public Emprestimos()
        {
            InitializeComponent();
            montaGrid();
            DataContext = this;
        }

        private void btnDevolver_Click(object sender, RoutedEventArgs e)
        {
            if (dtgEmprestimos.SelectedItem != null)
            {
                gerenciadorEmprestimos.devolverLivro(((Emprestimo)dtgEmprestimos.SelectedItem).Id);
                montaGrid();
            }
        }

        private void btnHistorico_Click(object sender, RoutedEventArgs e)
        {
            HistoricoModal historicoModal = new HistoricoModal();
            historicoModal.Show();
        }

        private void btnAddEmprestimo_Click(object sender, RoutedEventArgs e)
        {
            AdicionarEmrpestimoModal addEmprestimo = new AdicionarEmrpestimoModal();
            addEmprestimo.ShowDialog();
            if (addEmprestimo.Sucesso)
            {
                gerenciadorEmprestimos.addEmprestimo(addEmprestimo.Emprestimo);
                montaGrid();
            }
        }

        private void btnDeleteEmprestimo_Click(object sender, RoutedEventArgs e)
        {
            gerenciadorEmprestimos.deleteEmprestimo(((Emprestimo)dtgEmprestimos.SelectedItem).Id);
        }
    }
}
