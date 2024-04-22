using LibBusiness.Business.Core;
using OfficeOpenXml.Filter;
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
    /// Interaction logic for HistoricoModal.xaml
    /// </summary>
    public partial class HistoricoModal : Window
    {
        GerenciadorEmprestimos gerenciadorEmprestimos = new GerenciadorEmprestimos();
        public HistoricoModal()
        {
            InitializeComponent();
            dtgHistoricoEmprestimos.ItemsSource = gerenciadorEmprestimos.getEmprestimos();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
