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
    /// Interaction logic for ExcelHelpModal.xaml
    /// </summary>
    public partial class ExcelHelpModal : Window
    {
        private bool prosseguir;

        public bool Prosseguir
        {
            get { return prosseguir; }
            set { prosseguir = value; }
        }

        public ExcelHelpModal()
        {
            InitializeComponent();
            this.prosseguir=false;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            this.prosseguir = true;
            this.Close();
        }
    }
}
