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
    /// Interaction logic for AdicionarEmrpestimoModal.xaml
    /// </summary>
    public partial class AdicionarEmrpestimoModal : Window
    {
        GerenciadorLeitores gerenciadorLeitores = new GerenciadorLeitores();
        GerenciadorLivros gerenciadorLivros = new GerenciadorLivros();

        private bool sucesso;

        public bool Sucesso
        {
            get { return sucesso; }
            set { sucesso = value; }
        }

        private Emprestimo emprestimo = new Emprestimo();

        public Emprestimo Emprestimo
        {
            get { return emprestimo; }
            set { emprestimo = value; }
        }
        public AdicionarEmrpestimoModal()
        {
            InitializeComponent();
            sucesso = false;

            dateEmprestimo.SelectedDate = DateTime.Today;
            dateDevolucao.SelectedDate = DateTime.Today.AddDays(15);

            dtgLivro.ItemsSource = gerenciadorLivros.getLivros();

            dtgLeitor.ItemsSource = gerenciadorLeitores.getLeitores();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void cbxSearchLivro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxSearchLivro != null && txtSearchLivro != null)
            {
                string resultCbx = ((ComboBoxItem)cbxSearchLivro.SelectedItem).Content.ToString();
                dtgLivro.ItemsSource = gerenciadorLivros.filtrarLivros(resultCbx, txtSearchLivro.Text);
            }
        }

        private void txtSearchLivro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbxSearchLivro != null && txtSearchLivro != null)
            {
                string resultCbx = ((ComboBoxItem)cbxSearchLivro.SelectedItem).Content.ToString();
                dtgLivro.ItemsSource = gerenciadorLivros.filtrarLivros(resultCbx, txtSearchLivro.Text);
            }
        }

        private void cbxSearchLeitor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxSearchLeitor != null && txtSearchLeitor != null)
            {
                string resultCbx = ((ComboBoxItem)cbxSearchLeitor.SelectedItem).Content.ToString();
                dtgLeitor.ItemsSource = gerenciadorLeitores.filtrarLeitores(resultCbx, txtSearchLeitor.Text);
            }
        }

        private void txtSearchLeitor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbxSearchLeitor != null && txtSearchLeitor != null)
            {
                string resultCbx = ((ComboBoxItem)cbxSearchLeitor.SelectedItem).Content.ToString();
                dtgLeitor.ItemsSource = gerenciadorLeitores.filtrarLeitores(resultCbx, txtSearchLeitor.Text);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dtgLivro.SelectedItem != null && dtgLeitor.SelectedItem != null)
            {
                emprestimo.DataEmprestimo = dateEmprestimo.SelectedDate ?? DateTime.Today;
                emprestimo.DataDevolucao = dateDevolucao.SelectedDate ?? DateTime.Today.AddDays(15);
                emprestimo.LivroId = ((ViewLivroModel)dtgLivro.SelectedValue).Id;
                emprestimo.LeitorId = ((Leitor)dtgLeitor.SelectedValue).Id;
                emprestimo.Devolvido = false;
                sucesso = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Não foi possivel adicionar, verifique se os  campos estao preenchidos corretamente e/ou livro e leitor estao selecionados");
            }
        }
    }
}
